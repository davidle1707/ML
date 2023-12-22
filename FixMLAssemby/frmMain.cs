using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace FixMLAssemby
{
	public partial class frmMain : Form
	{
		readonly BindingList<FixAssembly> _fixAssemblies = new BindingList<FixAssembly>();

		readonly BindingList<FixProject> _fixProjects = new BindingList<FixProject>();

		public frmMain()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			dgAssemblies.DataSource = _fixAssemblies;
			dgProjects.DataSource = _fixProjects;
		}

		private void btnAssemblyFilePath_Click(object sender, EventArgs e)
		{
			if (dlgAssembyFilePath.ShowDialog() == DialogResult.OK)
			{
				foreach (var fileName in dlgAssembyFilePath.FileNames)
				{
					var fixAssembly = Assembly.LoadFile(fileName);
					var fixAssemblyName = fixAssembly.GetName();

					if (_fixAssemblies.All(p => !p.Name.Equals(fixAssemblyName.Name, StringComparison.OrdinalIgnoreCase)))
					{
						_fixAssemblies.Add(new FixAssembly
						{
							Name = fixAssemblyName.Name,
							Runtime = fixAssembly.ImageRuntimeVersion,
							Version = fixAssemblyName.Version.ToString(),
                            Description = string.Format("{0}, processorArchitecture={1}", fixAssemblyName, fixAssemblyName.ProcessorArchitecture),
							Path = fixAssembly.Location
						});
					}
				}
			}
		}

		private void btnBrowserSourcePath_Click(object sender, EventArgs e)
		{
			if (dlgSourcePath.ShowDialog() == DialogResult.OK)
			{
				tbxSourePath.Text = dlgSourcePath.SelectedPath;

				_fixProjects.Clear();

				foreach (var projectFile in Directory.GetFiles(tbxSourePath.Text, "*.csproj", SearchOption.AllDirectories))
				{
					if (_fixProjects.All(p => !p.Path.Equals(projectFile, StringComparison.OrdinalIgnoreCase)))
					{
						_fixProjects.Add(new FixProject
						{
							Selected = true,
							Path = projectFile
						});
					}
				}
			}
		}

		private void btnFixAssembly_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(tbxAssemblyFolderName.Text))
			{
				MessageBox.Show("Input assembly folder name.");
				tbxAssemblyFolderName.Focus();
				return;
			}

			if (_fixAssemblies.Count == 0)
			{
				MessageBox.Show("Select assembies to fix.");
				btnAssemblyFilePath.Focus();
				return;
			}

			if (_fixProjects.All(p => !p.Selected))
			{
				MessageBox.Show("Select projects to fix.");
				btnBrowserSourcePath.Focus();
				return;
			}

			if (MessageBox.Show("Are you sure you want to fix assemblies for those projectes", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
			{
				return;
			}

			FixAssemblies();
			
			MessageBox.Show("Fix ML assembly successful.");
		}

		private void dgAssemblies_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			var grid = (DataGridView)sender;

			if (grid.Columns[e.ColumnIndex].Name == colAssemblyRemove.Name && e.RowIndex >= 0)
			{
				_fixAssemblies.RemoveAt(e.RowIndex);
			}
		}

		private void FixAssemblies()
		{
			foreach (var fixProject in _fixProjects)
			{
				var doc = new XmlDocument();
				doc.Load(fixProject.Path);

				var itemGroup = doc.DocumentElement.Cast<XmlNode>().FirstOrDefault(n => n.Name == "ItemGroup" && n.ChildNodes.Cast<XmlNode>().Any(cn => cn.Name == "Reference"));

			    if (itemGroup == null)
			    {
			        continue;
			    }

			    var hasChanged = false;
			    var references = itemGroup.ChildNodes.Cast<XmlNode>();

			    foreach (var fixAssembly in _fixAssemblies)
			    {
			        var reference = references.FirstOrDefault(cn => IsMatchAssembly(cn, fixAssembly.Name));

			        if (reference == null)
			        {
			            continue;
			        }

			        var hintPath = reference["HintPath"];
			        var newHintPath = GetNewHintPath(hintPath.InnerText, fixAssembly.Path);

			        if (!string.IsNullOrWhiteSpace(newHintPath))
			        {
			            hasChanged = true;
			            reference.Attributes["Include"].Value = fixAssembly.Description;
			            hintPath.InnerText = newHintPath;
			        }
			    }

			    if (hasChanged)
			    {
			        doc.Save(fixProject.Path);
			    }
			}
		}

		private bool IsMatchAssembly(XmlNode reference, string assemblyName)
		{
			var includes = reference.Attributes["Include"].Value.Split(new[]{','}, StringSplitOptions.RemoveEmptyEntries);

			return includes[0].Equals(assemblyName, StringComparison.OrdinalIgnoreCase);
		}

		private string GetNewHintPath(string hintPath, string newReferenceMLPath)
		{
			//D:\Projects\ML\References\xxx.dll --> ML\References\xxx.dll
			var indexML = newReferenceMLPath.IndexOf(string.Format(@"{0}\", tbxAssemblyFolderName.Text), StringComparison.OrdinalIgnoreCase);
			var referenceMLPath = indexML >= 0 ? newReferenceMLPath.Substring(indexML) : string.Empty;

			if (string.IsNullOrWhiteSpace(referenceMLPath))
			{
				return string.Empty;
			}
			
			//..\..\..\..\ML\References\xxx.dll --> ..\..\..\..\
			var tmp = hintPath.Split(new[] { @"..\" }, StringSplitOptions.RemoveEmptyEntries)[0];
			var subHintPath = hintPath.Replace(tmp, "");

			return subHintPath + referenceMLPath;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_fixAssemblies.Clear();
			_fixProjects.Clear();

			tbxSourePath.Text = string.Empty;
		}

	}

	public class FixAssembly
	{
		public string Name { get; set; }

		public string Runtime { get; set; }

		public string Version { get; set; }

		public string Description { get; set; }

		public string Path { get; set; }
	}

	public class FixProject
	{
		public bool Selected { get; set; }

		public string Path { get; set; }
	}
}
