using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;

namespace ML.Utils.Phone.Vendors.Tropo
{
    public class Tropo
    {
        [JsonProperty(PropertyName = "tropo")]
        public IList<string> ActionElements { get; private set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public Tropo()
        {
            ActionElements = new List<string>();
        }

        /// <summary>
        /// Set a default voice for use with all Text To Speech.
        /// </summary>
        public string Voice { get; set; }

        /// <summary>
        /// Set a default language to use in speech recognition.
        /// </summary>
        public string Language { get; set; }

        public string RenderJSON()
        {
            this.Language = null;
            this.Voice = null;

            var settings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore };
            return JsonConvert.SerializeObject(this, Formatting.None, settings).Replace("\\", "").Replace("\"{", "{").Replace("}\"", "}");
        }
    }

    /// <summary>
    /// Create an instance of the Tropo Session object.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="json">Session JSON submitted from Tropo platform.</param>
        public Session(string json)
        {
            var session = JObject.Parse(json);
            AccountId = (string)session["session"]["accountId"];
            Id = (string)session["session"]["id"];
            InitialText = (string)session["session"]["initialText"];
            Timestamp = (string)session["session"]["timestamp"];
            UserType = (string)session["session"]["userType"];

            if (session["session"]["from"] != null)
            {
                var fromId = (string)session["session"]["from"]["id"];
                var fromName = (string)session["session"]["from"]["name"];
                var fromNetwork = (string)session["session"]["from"]["network"];
                var fromChannel = (string)session["session"]["from"]["channel"];
                From = new Endpoint(fromId, fromChannel, fromName, fromNetwork);
            }

            if (session["session"]["to"] != null)
            {
                var toId = (string)session["session"]["to"]["id"];
                var toName = (string)session["session"]["to"]["name"];
                var toNetwork = (string)session["session"]["to"]["network"];
                var toChannel = (string)session["session"]["to"]["channel"];
                To = new Endpoint(toId, toChannel, toName, toNetwork);
            }

            if (session["session"]["parameters"] != null)
            {
                Parameters = new NameValueCollection();
                var _parameter = session["session"]["parameters"].First;

                while (_parameter != null)
                {
                    var property = (JProperty)_parameter;
                    Parameters.Add(property.Name, (string)property.Value);
                    _parameter = _parameter.Next;
                }
            }

            if (session["session"]["headers"] != null)
            {
                Headers = new NameValueCollection();
                var _header = session["session"]["headers"].First;

                while (_header != null)
                {
                    var property = (JProperty)_header;
                    Headers.Add(property.Name, property.Value.ToString());
                    _header = _header.Next;
                }
            }

        }

        /// <summary>
        /// Contains the user account ID that started this session.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Contains the elements that identify the origination of the session.
        /// </summary>
        public Endpoint From { get; set; }

        /// <summary>
        /// Contains the GUID representing the unique session identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// When the channel is of a type "TEXT", this field contains the initial text of the message from the SMS 
        /// or instant message that the user sent when initiating the session.
        /// </summary>
        public string InitialText { get; set; }

        /// <summary>
        /// The time that the session was started.
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// The SIP destination for the incoming call.
        /// </summary>
        public Endpoint To { get; set; }

        /// <summary>
        /// Identifies the type of user that is on the other end of the session; it can be set to 'HUMAN', 'MACHINE' or 'FAX'.
        /// </summary>
        public string UserType { get; set; }

        /// <summary> 
        /// Identifies the parameters passed in to the session. 
        /// </summary> 
        public NameValueCollection Parameters { get; set; }

        /// <summary> 
        /// Identifies the SIP headers passed in to the session. 
        /// </summary> 
        public NameValueCollection Headers { get; set; }
    }

    /// <summary>
    /// Create an instance of the Tropo Result object
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="json">Result JSON submitted from Tropo platform</param>
        public Result(string json)
        {
            var results = JObject.Parse(json);
            SessionId = (string)results["result"]["sessionId"];
            State = (string)results["result"]["state"];
            SessionDuration = (int)results["result"]["sessionDuration"];
            Sequence = (int)results["result"]["sequence"];
            Complete = (bool)results["result"]["complete"];
            Error = (string)results["result"]["error"];
            Actions = (JContainer)results["result"]["actions"];
        }

        /// <summary>
        /// The state of the session at the time the result was generated.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The unique identifier that is available with each session and result payload.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// The total length of time, in seconds, the current session has been running.
        /// </summary>
        public int SessionDuration { get; set; }

        /// <summary>
        /// Represents the number of Tropo payloads returned from your application.
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// Indicates whether a request resulted in all required fields being completed.
        /// </summary>
        public bool Complete { get; set; }

        /// <summary>
        /// If the state of the result is an error, refer to this field for the error message.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// The result of the actions requested in the previous payload.
        /// </summary>
        public JContainer Actions { get; set; }
    }

    /// <summary>
    /// The Base class for all Tropo actions.
    /// </summary>
    public class TropoBase
    {
        protected TropoBase()
        {
        }
    }

    /// <summary>
    /// Ask is essentially a say that requires input; it requests information from the caller and waits for a response.
    /// </summary>
    public class Ask : TropoBase
    {
        [JsonProperty(PropertyName = "attempts")]
        public int? Attempts { get; set; }

        [JsonProperty(PropertyName = "allowSignals")]
        public Array AllowSignals { get; set; }

        [JsonProperty(PropertyName = "bargein")]
        public bool? Bargein { get; set; }

        [JsonProperty(PropertyName = "interdigitTimeout")]
        public bool? InterdigitTimeout { get; set; }

        [JsonProperty(PropertyName = "minConfidence")]
        public int? MinConfidence { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "recognizer")]
        public string Recognizer { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool? Required { get; set; }

        [JsonProperty(PropertyName = "choices")]
        public Choices Choices { get; set; }

        [JsonProperty(PropertyName = "say")]
        public Say Say { get; set; }

        [JsonProperty(PropertyName = "timeout")]
        public float? Timeout { get; set; }

        [JsonProperty(PropertyName = "voice")]
        public string Voice { get; set; }

        public Ask()
        {
        }

        public Ask(Choices choices, string name, Say say)
        {
            Choices = choices;
            Name = name;
            Say = say;
        }
    }

    /// <summary>
    /// Initiates an outbound call or a text conversation. Note that this action is only valid when there is no active WebAPI call.
    /// </summary>
    public class Call : TropoBase
    {
        [JsonProperty(PropertyName = "to")]
        public IEnumerable<string> To { get; set; }

        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }

        [JsonProperty(PropertyName = "network")]
        public string Network { get; set; }

        [JsonProperty(PropertyName = "channel")]
        public string Channel { get; set; }

        [JsonProperty(PropertyName = "answerOnMedia")]
        public bool? AnswerOnMedia { get; set; }

        [JsonProperty(PropertyName = "allowSignals")]
        public Array allowSignals { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "headers")]
        public IDictionary<string, string> Headers { get; set; }

        [JsonProperty(PropertyName = "recording")]
        public StartRecording Recording { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool? Required { get; set; }

        [JsonProperty(PropertyName = "timeout")]
        public float? Timeout { get; set; }

        public Call()
        {
        }

        public Call(string to)
        {
            To = new List<string> {to};
        }

        public Call(IEnumerable<string> to)
        {
            To = to;
        }
    }

    /// <summary>
    /// The grammar to use in recognizing and validating input.
    /// </summary>
    public class Choices : TropoBase
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "mode")]
        public string Mode { get; set; }

        [JsonProperty(PropertyName = "terminator")]
        public string Terminator { get; set; }

        public Choices()
        {
        }

        public Choices(string @value)
        {
            Value = @value;
        }

        public Choices(string @value, string mode, string terminator)
        {
            Value = @value;
            Mode = mode;
            Terminator = terminator;
        }
    }

    /// <summary>
    /// This action allows multiple lines in separate sessions to be conferenced together so that the parties on each line can talk to each other simultaneously.
    /// </summary>
    public class Conference : TropoBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "allowSignals")]
        public Array allowSignals { get; set; }

        [JsonProperty(PropertyName = "mute")]
        public bool? Mute { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "playTones")]
        public bool? PlayTones { get; set; }

        [JsonProperty(PropertyName = "terminator")]
        public string Terminator { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool? Required { get; set; }

        public Conference()
        {
        }
    }

    /// <summary>
    /// This action instructs Tropo to "hang-up" or disconnect the session associated with the current session. 
    /// </summary>
    public class Hangup : TropoBase
    {
        public Hangup()
        {
        }
    }

    /// <summary>
    /// Creates a call, says something and then hangs up, all in one step. This is particularly useful for sending out a quick SMS or IM. 
    /// </summary>
    public class Message : TropoBase
    {
        [JsonProperty(PropertyName = "say")]
        public Say Say { get; set; }

        [JsonProperty(PropertyName = "to")]
        public IEnumerable<string> To { get; set; }

        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }

        [JsonProperty(PropertyName = "network")]
        public string Network { get; set; }

        [JsonProperty(PropertyName = "channel")]
        public string Channel { get; set; }

        [JsonProperty(PropertyName = "answerOnMedia")]
        public bool? AnswerOnMedia { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool? Required { get; set; }

        [JsonProperty(PropertyName = "timeout")]
        public float? Timeout { get; set; }

        [JsonProperty(PropertyName = "voice")]
        public string Voice { get; set; }

        public Message()
        {
        }
    }

    /// <summary>
    /// This action determines the event(s) to be handled. 
    /// </summary>
    public class On : TropoBase
    {
        [JsonProperty(PropertyName = "event")]
        public string Event { get; set; }

        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool? Required { get; set; }

        [JsonProperty(PropertyName = "say")]
        public Say Say { get; set; }

        public On()
        {
        }

        public On(string @event, string next, Say say)
        {
            Event = @event;
            Next = next;
            Say = say;
        }
    }

    /// <summary>
    /// Plays a prompt (audio file or text to speech) then optionally waits for a response from the caller and records it.
    /// </summary>
    public class Record : TropoBase
    {
        [JsonProperty(PropertyName = "attempts")]
        public int? Attempts { get; set; }

        [JsonProperty(PropertyName = "allowSignals")]
        public Array allowSignals { get; set; }

        [JsonProperty(PropertyName = "bargein")]
        public bool? Bargein { get; set; }

        [JsonProperty(PropertyName = "beep")]
        public bool? Beep { get; set; }

        [JsonProperty(PropertyName = "choices")]
        public Choices Choices { get; set; }

        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        [JsonProperty(PropertyName = "maxSilence")]
        public float? MaxSilence { get; set; }

        [JsonProperty(PropertyName = "maxTime")]
        public float? MaxTime { get; set; }

        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "minConfidence")]
        public int? MinConfidence { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool? Required { get; set; }

        [JsonProperty(PropertyName = "say")]
        public Say Say { get; set; }

        [JsonProperty(PropertyName = "timeout")]
        public float? Timeout { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "transcription")]
        public Transcription Transcription { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "voice")]
        public string Voice { get; set; }

        public Record()
        {
        }
    }

    /// <summary>
    /// This is used to deflect the call to a third party SIP address. This action must be called before the call is answered.
    /// </summary>
    public class Redirect : TropoBase
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool? Required { get; set; }

        [JsonProperty(PropertyName = "to")]
        public IEnumerable<string> To { get; set; }

        public Redirect()
        {
        }
    }

    /// <summary>
    /// Reject an incoming call.
    /// </summary>
    public class Reject : TropoBase
    {
        public Reject()
        {
        }
    }

    /// <summary>
    /// When the current session is a voice channel this key will either play a message or an audio file from a URL. 
    /// In the case of an text channel it will send the text back to the user via instant messaging or SMS.
    /// </summary>
    public class Say : TropoBase
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "allowSignals")]
        public Array allowSignals { get; set; }

        [JsonProperty(PropertyName = "as")]
        public string As { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool? Required { get; set; }

        [JsonProperty(PropertyName = "voice")]
        public string Voice { get; set; }

        public Say()
        {
        }

        public Say(string @value)
        {
            Value = @value;
        }
    }

    /// <summary>
    /// Allows Tropo applications to begin recording the current session. 
    /// </summary>
    public class StartRecording : TropoBase
    {
        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        public StartRecording()
        {
        }

        public StartRecording(string format, string method, string url, string username, string password)
        {
            Format = format;
            Method = method;
            Url = url;
            Username = username;
            Password = password;
        }
    }

    /// <summary>
    /// This action stops the recording of the current call after startCallRecording has been called. 
    /// </summary>
    public class StopRecording : TropoBase
    {
        public StopRecording()
        {
        }
    }

    /// <summary>
    /// Transcribes spoken text.
    /// </summary>
    public class Transcription : TropoBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "emailFormat")]
        public string EmailFormat { get; set; }

        public Transcription()
        {
        }
    }

    /// <summary>
    /// This will transfer an already answered call to another destination / phone number. 
    /// </summary>
    public class Transfer : TropoBase
    {
        [JsonProperty(PropertyName = "to")]
        public IEnumerable<string> To { get; set; }

        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }

        [JsonProperty(PropertyName = "answerOnMedia")]
        public bool? AnswerOnMedia { get; set; }

        [JsonProperty(PropertyName = "allowSignals")]
        public Array allowSignals { get; set; }

        [JsonProperty(PropertyName = "choices")]
        public Choices Choices { get; set; }

        [JsonProperty(PropertyName = "headers")]
        public IDictionary<string, string> Headers { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "on")]
        public On On { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool? Required { get; set; }

        [JsonProperty(PropertyName = "terminator")]
        public string Terminator { get; set; }

        [JsonProperty(PropertyName = "timeout")]
        public float? Timeout { get; set; }

        public Transfer()
        {
        }
    }

    /// <summary>
    /// Defnies an endoint for transfer and redirects.
    /// </summary>
    public class Endpoint
    {
        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "channel")]
        public string Channel { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "network")]
        public string Network { get; set; }

        public Endpoint()
        {
        }

        public Endpoint(string id, string channel, string name, string network)
        {
            Id = id;
            Channel = channel;
            Name = name;
            Network = network;
        }

        public Endpoint(string to)
        {
            To = to;
        }
    }
}
