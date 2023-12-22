using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ML.Common
{
    public static class TaskScheduleHelper
    {
        public static void Process(TaskSetting settingOrNull, Action<TaskService> action)
        {
            var taskService = settingOrNull != null && settingOrNull.IsValid()
                ? new TaskService(settingOrNull.Server, settingOrNull.UserName, settingOrNull.UserDomain, settingOrNull.UserPassword)
                : new TaskService();

            using (taskService)
            {
                action(taskService);
            }
        }

        public static TResult Process<TResult>(TaskSetting settingOrNull, Func<TaskService, TResult> func)
        {
            var taskService = settingOrNull != null && settingOrNull.IsValid()
                ? new TaskService(settingOrNull.Server, settingOrNull.UserName, settingOrNull.UserDomain, settingOrNull.UserPassword)
                : new TaskService();

            using (taskService)
            {
                return func(taskService);
            }
        }

        public static bool CreateFolder(string folder, TaskSetting setting = null)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                return false;
            }

            Process(setting, service => service.RootFolder.CreateFolder(folder));

            return true;
        }

        public static string GetTaskFullPath(string folder, string name)
        {
            return !string.IsNullOrWhiteSpace(folder) ? Path.Combine(folder, name) : name;
        }

        public static Task CreateTask(string folder, string taskName, string description, short daysInterval, DateTime start, int repeatMinutes, string executePath, string argument, Action<TaskDefinition> extendAction = null, TaskSetting setting = null)
        {
            return Process(setting, taskService =>
            {
                var taskDefinition = CreateBaseTaskDefinition(taskService, executePath, argument, description);

                var trigger = new DailyTrigger(daysInterval)
                {
                    StartBoundary = start,
                    Enabled = true
                };

                trigger.SetRepetition(TimeSpan.FromMinutes(repeatMinutes), TimeSpan.FromDays(1));

                taskDefinition.Triggers.Add(trigger);

                if (extendAction != null)
                {
                    extendAction(taskDefinition);
                }

                var taskPath = GetTaskFullPath(folder, taskName);
                var task = CreateTask(taskService, taskPath, taskDefinition, setting);

                return task;
            });
        }

        public static Task CreateOneTimeTask(string folder, string taskName, string description, DateTime start, string executePath, string argument, Action<TaskDefinition> extendAction = null, TaskSetting setting = null)
        {
            return Process(setting, taskService =>
            {
                var taskDefinition = CreateBaseTaskDefinition(taskService, executePath, argument, description);

                var trigger = new TimeTrigger
                {
                    StartBoundary = start,
                    Enabled = true
                };

                taskDefinition.Triggers.Add(trigger);

                if (extendAction != null)
                {
                    extendAction(taskDefinition);
                }

                var taskPath = GetTaskFullPath(folder, taskName);
                var task = CreateTask(taskService, taskPath, taskDefinition, setting);

                return task;
            });
        }

        public static void DeleteTask(string folder, string taskName, bool exceptionOnNotExists = false, TaskSetting setting = null)
        {
            if (!string.IsNullOrWhiteSpace(taskName))
            {
                var taskPath = GetTaskFullPath(folder, taskName);

                Process(setting, taskService => taskService.RootFolder.DeleteTask(taskPath, exceptionOnNotExists));
            }
        }

        public static void DeleteTasks(string folder, List<string> taskNames, bool exceptionOnNotExists = false, TaskSetting setting = null)
        {
            Process(setting, taskService =>
                             {
                                 foreach (var taskName in taskNames.Where(t => !string.IsNullOrWhiteSpace(t)))
                                 {
                                     var taskPath = GetTaskFullPath(folder, taskName);
                                     taskService.RootFolder.DeleteTask(taskPath, exceptionOnNotExists);
                                 }
                             });
        }

        public static void DeleteTasks(string folder, Regex filter = null, Func<Task, bool> condition = null,  bool exceptionOnNotExists = false, TaskSetting setting = null)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                return;
            }

            Process(setting, service =>
            {
                var tfolder = service.GetFolder(folder);

                if (tfolder == null)
                {
                    return;
                }

                var tasks = tfolder.GetTasks(filter);

                foreach (var task in tasks)
                {
                    var ok = condition?.Invoke(task) ?? true;

                    if (ok)
                    {
                        tfolder.DeleteTask(task.Name, exceptionOnNotExists);
                    }
                }
            });
        }

        public static bool ExistsFolder(string folder, TaskSetting setting = null)
        {
            return !string.IsNullOrWhiteSpace(folder) && Process(setting, taskService => taskService.GetFolder(folder) != null);
        }

        public static Task FindTask(string folder, string taskName, TaskSetting setting = null)
        {
            if (!string.IsNullOrWhiteSpace(taskName))
            {
                var taskPath = GetTaskFullPath(folder, taskName);

                return Process(setting, taskService => taskService.GetTask(taskPath));
            }

            return null;
        }

        public static bool ExistsTask(string folder, string taskName, TaskSetting setting = null)
        {
            var task = FindTask(folder, taskName, setting);

            return task != null;
        }

        private static TaskDefinition CreateBaseTaskDefinition(TaskService taskService, string executePath, string argument = "", string description = "")
        {
            var task = taskService.NewTask();

            task.RegistrationInfo.Description = description;

            task.Principal.LogonType = TaskLogonType.S4U;
            task.Principal.RunLevel = TaskRunLevel.Highest;

            task.Settings.Hidden = true;
            task.Settings.Enabled = true;
            task.Settings.ExecutionTimeLimit = TimeSpan.FromDays(2); //stop the task if it runs longer than: xxx
            task.Settings.AllowHardTerminate = false; //if the running tasks does not end when requested, force it to stop

            task.Actions.Add(new ExecAction(executePath, argument));

            return task;
        }

        private static Task CreateTask(TaskService service, string path, TaskDefinition definition, TaskSetting setting = null)
        {
            if (setting != null && setting.IsValid())
            {
                var userId = string.Format(@"{0}\{1}", setting.UserDomain, setting.UserName);

                return service.RootFolder.RegisterTaskDefinition(path, definition, setting.CreateType, userId, setting.UserPassword, setting.CreateLogonType);
            }

            return service.RootFolder.RegisterTaskDefinition(path, definition);
        }
    }

    [Serializable]
    public class TaskSetting
    {
        public TaskSetting()
        {
            Server = "127.0.0.1";

            CreateType = TaskCreation.Create;

            CreateLogonType = TaskLogonType.S4U;
        }

        /// <summary>
        /// default: 127.0.0.1
        /// </summary>
        public string Server { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string UserDomain { get; set; }

        public TaskCreation CreateType { get; set; }

        public TaskLogonType CreateLogonType { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Server) && !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(UserPassword) && !string.IsNullOrWhiteSpace(UserDomain);
        }
    }
}
