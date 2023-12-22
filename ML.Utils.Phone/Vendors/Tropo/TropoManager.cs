using System.Linq;
using ML.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ML.Utils.Phone.Vendors.Tropo
{
	/// <summary>
	/// The main tropo class.
	/// </summary>
	public class TropoManager : VendorManager
	{
		public TropoSetting Setting;

		public Tropo Tropo { get; set; }

		public TropoManager()
		{
			Tropo = new Tropo();
		}

		public TropoManager(bool fromAppSetting)
			: this()
		{
			var setting = new TropoSetting(fromAppSetting);

			Init(setting);
		}

		public TropoManager(TropoSetting setting)
			: this()
		{
			Init(setting);
		}

		public void Init(TropoSetting setting)
		{
			Setting = setting ?? new TropoSetting();

			if (!Setting.IsValid())
			{
				throw new TropoException("Tropo setting is invalid.");
			}
		}

		#region Ask

		/// <summary>
		/// Sends a prompt to the user and optionally waits for a response. 
		/// </summary>
		/// <param name="attempts">How many times the caller can attempt input before an error is thrown.</param>
		/// <param name="bargein">Should the user be allowed to barge in before TTS is complete?</param>
		/// <param name="choices">The grammar to use in recognizing and validating input.</param>
		/// <param name="minConfidence">How confident should Tropo be in a speech recognition match?</param>
		/// <param name="name">identifies the return value of an ask, so you know the context for the returned information.</param>
		/// <param name="required">Is input required here?</param>
		/// <param name="say">This determines what is played or sent to the caller.</param>
		/// <param name="timeout">The amount of time Tropo will wait, in seconds, after sending or playing the prompt for the user to begin a response.</param>
		public void Ask(int? attempts, bool? bargein, Choices choices, int? minConfidence, string name, bool? required, Say say, float? timeout)
		{
			var ask = new Ask();
			ask.Attempts = attempts;
			ask.Bargein = bargein;
			ask.Choices = choices;
			ask.MinConfidence = minConfidence;
			ask.Name = name;
			ask.Required = required;
			ask.Voice = string.IsNullOrEmpty(this.Tropo.Voice) ? null : this.Tropo.Voice;
			ask.Say = say;
			ask.Timeout = timeout;

			Serialize(ask, "ask");
		}

		/// <summary>
		/// Overload method for Ask that allows events to be set via allowSignals.
		/// </summary>
		/// <param name="attempts">How many times the caller can attempt input before an error is thrown.</param>
		/// <param name="allowSignals">Allows for the assignment of an interruptable signal for this Tropo function</param>
		/// <param name="bargein">Should the user be allowed to barge in before TTS is complete?</param>
		/// <param name="interdigitTimeout">Defines how long to wait - in seconds - between key presses to determine the user has stopped entering input.</param>
		/// <param name="choices">The grammar to use in recognizing and validating input</param>
		/// <param name="minConfidence">How confident should Tropo be in a speech reco match?</param>
		/// <param name="name">Identifies the return value of an Ask, so you know the context for the returned information.</param>
		/// <param name="recognizer">Tells Tropo what language to listen for</param>
		/// <param name="required">Is input required here?</param>
		/// <param name="say">This determines what is played or sent to the caller.</param>
		/// <param name="timeout">The amount of time Tropo will wait, in seconds, after sending or playing the prompt for the user to begin a response.</param>
		public void Ask(int? attempts, Array allowSignals, bool? bargein, int? interdigitTimeout, Choices choices, int? minConfidence, string name, string recognizer, bool? required, Say say, float? timeout)
		{
			var ask = new Ask();
			ask.Attempts = attempts;
			ask.AllowSignals = allowSignals;
			ask.Bargein = bargein;
			ask.Choices = choices;
			ask.MinConfidence = minConfidence;
			ask.Name = name;
			ask.Required = required;
			ask.Voice = string.IsNullOrEmpty(this.Tropo.Voice) ? null : this.Tropo.Voice;
			ask.Say = say;
			ask.Timeout = timeout;

			Serialize(ask, "ask");
		}

		/// <summary>
		/// Overload for Ask that allows an Ask object to be passed.
		/// </summary>
		/// <param name="ask">An Ask object.</param>
		public void Ask(Ask ask)
		{
			Ask(ask.Attempts, ask.Bargein, ask.Choices, ask.MinConfidence, ask.Name, ask.Required, ask.Say, ask.Timeout);
		}

		#endregion

		#region Call

		/// <summary>
		/// Places a call or sends an an IM, Twitter, or SMS message. To start a call, use the Session API to tell Tropo to launch your code.
		/// </summary>
		/// <param name="to">The party(ies)to call.</param>
		/// <param name="from">A string representing who the call is from.</param>
		/// <param name="network">Network is used mainly by the text channels; values can be SMS when sending a text message, or a valid IM network name such as AIM, MSN, JABBER, YAHOO and GTALK.</param>
		/// <param name="channel">This defines the channel used to place new calls.</param>
		/// <param name="answerOnMedia">If this is set to true, the call will be considered "answered" and audio will begin playing as soon as media is received from the far end </param>
		/// <param name="timeout">The amount of time Tropo will wait, in seconds, after sending or playing the prompt for the user to begin a response.</param>
		/// <param name="headers">This contains the Session Initiation Protocol (SIP) Headers for the current session.</param>
		/// <param name="recording">This is a shortcut to allow you to start call recording as soon as the call is answered. </param>      
		public void Call(IEnumerable<string> to, string from, string network, string channel, bool? answerOnMedia, float? timeout, IDictionary<string, string> headers, StartRecording recording)
		{
			var call = new Call();
			call.To = to;
			call.From = from;
			call.Network = network;
			call.Channel = channel;
			call.AnswerOnMedia = answerOnMedia;
			call.Timeout = timeout;
			call.Headers = headers;
			call.Recording = recording;

			Serialize(call, "call");
		}

		/// <summary>
		/// Overload for Call that allows the To parameter to be passed as a String.
		/// </summary>
		/// <param name="to">A String containing the recipient to call.</param>
		/// <param name="from">A string representing who the call is from.</param>
		/// <param name="network">Network is used mainly by the text channels; values can be SMS when sending a text message, or a valid IM network name such as AIM, MSN, JABBER, YAHOO and GTALK.</param>
		/// <param name="channel">This defines the channel used to place new calls.</param>
		/// <param name="answerOnMedia">If this is set to true, the call will be considered "answered" and audio will begin playing as soon as media is received from the far end.</param>
		/// <param name="timeout">The amount of time Tropo will wait, in seconds, after sending or playing the prompt for the user to begin a response.</param>
		/// <param name="headers">This contains the Session Initiation Protocol (SIP) Headers for the current session.</param>
		/// <param name="recording">This is a shortcut to allow you to start call recording as soon as the call is answered. </param>      
		public void Call(string to, string from, string network, string channel, bool? answerOnMedia, float? timeout, IDictionary<string, string> headers, StartRecording recording)
		{
			var call = new Call
			{
				To = new List<string> { to },
				From = from,
				Network = network,
				Channel = channel,
				AnswerOnMedia = answerOnMedia,
				Timeout = timeout,
				Headers = headers,
				Recording = recording
			};

			Serialize(call, "call");
		}

		/// <summary>
		/// Overload for Call that allows events to be set via allowSignals.
		/// </summary>
		/// <param name="to"></param>
		/// <param name="allowSignals">Allows for the assignment of an interruptable signal for this Tropo function</param>
		/// <param name="from"></param>
		/// <param name="network"></param>
		/// <param name="channel"></param>
		/// <param name="answerOnMedia"></param>
		/// <param name="timeout"></param>
		/// <param name="headers"></param>
		/// <param name="recording"></param>
		public void Call(string to, Array allowSignals, string from, string network, string channel, bool? answerOnMedia, float? timeout, IDictionary<string, string> headers, StartRecording recording)
		{
			var call = new Call
			{
				To = new List<string> { to },
				allowSignals = allowSignals,
				From = from,
				Network = network,
				Channel = channel,
				AnswerOnMedia = answerOnMedia,
				Timeout = timeout,
				Headers = headers,
				Recording = recording
			};

			Serialize(call, "call");
		}

		/// <summary>
		/// Overload for Call that allows one parameter.
		/// </summary>
		/// <param name="to">The number of the person to call.</param>
		public void Call(string to)
		{
			var call = new Call(to);
			Serialize(call, "call");
		}

		/// <summary>
		/// Overload for call that allows multiple calls to be made with one parmameter.
		/// </summary>
		/// <param name="to">An ArryList containing recipients to call.</param>
		public void Call(IEnumerable<string> to)
		{
			var call = new Call(to);
			Serialize(call, "call");
		}

		/// <summary>
		/// Overload for call that allows a Call object to be passed.
		/// </summary>
		/// <param name="call">A Call object.</param>
		public void Call(Call call)
		{
			Call(call.To, call.From, call.Network, call.Channel, call.AnswerOnMedia, call.Timeout, call.Headers, call.Recording);
		}

		#endregion

		#region Conference

		/// <summary>
		/// This object allows multiple lines in separate sessions to be conferenced together so that the parties on each line can talk to each other simultaneously.
		/// This is a voice channel only feature. 
		/// </summary>
		/// <param name="id">This defines the id/name of the conference room to create.</param>
		/// <param name="mute">Adds the user to the conference room with their audio muted.</param>
		/// <param name="name">Identifies the return value of a Conference, so you know the context for the returned information.</param>
		/// <param name="playTones">This defines whether to send touch tone phone input to the conference or block the audio.</param>
		/// <param name="required">Determines whether Tropo should move on to the next action.</param>
		/// <param name="terminator">This is the touch-tone key (also known as "DTMF digit") used to exit the conference.</param>
		public void Conference(string id, bool? mute, string name, bool? playTones, bool? required, string terminator)
		{
			var conference = new Conference();
			conference.Id = id;
			conference.Mute = mute;
			conference.Name = name;
			conference.PlayTones = playTones;
			conference.Required = required;
			conference.Terminator = terminator;

			Serialize(conference, "conference");
		}

		/// <summary>
		/// Overload for Conference that allows events to be set via allowSignals.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="allowSignals">Allows for the assignment of an interruptable signal for this Tropo function</param>
		/// <param name="mute"></param>
		/// <param name="name"></param>
		/// <param name="playTones"></param>
		/// <param name="required"></param>
		/// <param name="terminator"></param>
		public void Conference(string id, Array allowSignals, bool? mute, string name, bool? playTones, bool? required, string terminator)
		{
			var conference = new Conference();
			conference.Id = id;
			conference.allowSignals = allowSignals;
			conference.Mute = mute;
			conference.Name = name;
			conference.PlayTones = playTones;
			conference.Required = required;
			conference.Terminator = terminator;

			Serialize(conference, "conference");
		}

		/// <summary>
		/// Overload for Conference that allows a Conference object to be passed.
		/// </summary>
		/// <param name="conference">A Conference object.</param>
		public void Conference(Conference conference)
		{
			Conference(conference.Id, conference.Mute, conference.Name, conference.PlayTones, conference.Required, conference.Terminator);
		}

		#endregion

		#region Hangup

		/// <summary>
		/// This function instructs Tropo to "hang-up" or disconnect the session associated with the current session.
		/// </summary>
		public void Hangup()
		{
			var hangup = new Hangup();
			Serialize(hangup, "hangup");
		}

		#endregion

		#region Message

		/// <summary>
		/// A shortcut method to create a session, say something, and hang up, all in one step. 
		/// This is particularly useful for sending out a quick SMS or IM. 
		/// </summary>
		/// <param name="say">This determines what is played or sent to the caller.</param>
		/// <param name="to">The destination to make a call to or send a message to.</param>
		/// <param name="answerOnMedia">If this is set to true, the call will be considered "answered" and audio will begin playing as soon as media is received from the far end.</param>
		/// <param name="channel">This defines the channel used to place new calls.</param>
		/// <param name="from">A string representing who the call is from.</param>
		/// <param name="name">Identifies the return value of a Message, so you know the context for the returned information.</param>
		/// <param name="network">Network is used mainly by the text channels; values can be SMS when sending a text message, or a valid IM network name such as AIM, MSN, JABBER, YAHOO and GTALK.</param>
		/// <param name="required">Determines whether Tropo should move on to the next action.</param>
		/// <param name="timeout">The amount of time Tropo will wait, in seconds, after sending or playing the prompt for the user to begin a response.</param>
		public void Message(Say say, IEnumerable<string> to, bool? answerOnMedia, string channel, string from, string name, string network, bool? required, float? timeout)
		{
			var message = new Message();
			message.Say = say;
			message.To = to;
			message.AnswerOnMedia = answerOnMedia;
			message.Channel = channel;
			message.From = from;
			message.Name = name;
			message.Network = network;
			message.Required = required;
			message.Timeout = timeout;
			message.Voice = string.IsNullOrEmpty(this.Tropo.Voice) ? null : this.Tropo.Voice;

			Serialize(message, "message");
		}

		/// <summary>
		/// Overload for Message that allows a Message object to be passed.
		/// </summary>
		/// <param name="message">A Message object.</param>
		public void Message(Message message)
		{
			Message(message.Say, message.To, message.AnswerOnMedia, message.Channel, message.From, message.Name, message.Network, message.Required, message.Timeout);
		}

		#endregion

		#region On

		/// <summary>
		/// Adds an event callback so that your application may be notified when a particular event occurs.
		/// </summary>
		/// <param name="event">This defines which event the on action handles.</param>
		/// <param name="next">When an associated event occurs, Tropo will post to the URL defined here. If left blank, Tropo will simply hangup.</param>
		/// <param name="say">This determines what is played or sent to the caller.</param>      
		public void On(string @event, string next, Say say)
		{
			var on = new On();
			on.Event = @event;
			on.Next = next;
			on.Say = say;

			Serialize(on, "on");
		}

		/// <summary>
		/// Overload for On that allows an On object to be passed.
		/// </summary>
		/// <param name="on">An On object.</param>
		public void On(On on)
		{
			On(on.Event, on.Next, on.Say);
		}

		#endregion

		#region Record

		/// <summary>
		/// Plays a prompt (audio file or text to speech) and optionally waits for a response from the caller that is recorded.
		/// If collected, responses may be in the form of DTMF or speech recognition using a simple grammar format defined below.
		/// The record funtion is really an alias of the prompt function, but one which forces the record option to true regardless of how it is (or is not) initially set.
		/// At the conclusion of the recording, the audio file may be automatically sent to an external server via FTP or an HTTP POST/Multipart Form.
		/// If specified, the audio file may also be transcribed and the text returned to you via an email address or HTTP POST/Multipart Form.
		/// </summary>
		/// <param name="attempts">How many times the caller can attempt input before an error is thrown.</param>
		/// <param name="bargein">Should the user be allowed to barge in before TTS is complete?</param>
		/// <param name="beep">When set to true, callers will hear a tone indicating the recording has begun.</param>
		/// <param name="choices">The grammar to use in recognizing and validating input.</param>
		/// <param name="format">This specifies the format for the audio recording.</param>
		/// <param name="maxSilence">The maximum amount of time, in seconds, to wait for silence after a user stops speaking.</param>
		/// <param name="maxTime">The maximum amount of time, in seconds, the user is allotted for input.</param>
		/// <param name="method">This defines how you want to send the audio file.</param>
		/// <param name="password">This identifies the FTP account password.</param>
		/// <param name="required">Determines whether Tropo should move on to the next action.</param>
		/// <param name="say">This determines what is played or sent to the caller.</param>
		/// <param name="timeout">The amount of time Tropo will wait, in seconds, after sending or playing the prompt for the user to begin a response.</param>
		/// <param name="username">This identifies the FTP account username.</param>
		/// <param name="url">This is the destination URL to send the recording.</param>
		public void Record(int? attempts, bool? bargein, bool? beep, Choices choices, string format, float? maxSilence, float? maxTime, string method, string password, bool? required, Say say, float? timeout, string username, string url)
		{
			var record = new Record();
			record.Attempts = attempts;
			record.Bargein = bargein;
			record.Beep = beep;
			record.Choices = choices;
			record.Format = format;
			record.MaxSilence = maxSilence;
			record.MaxTime = maxTime;
			record.Method = method;
			record.Password = password;
			record.Required = required;
			record.Say = say;
			record.Timeout = timeout;
			record.Url = url;
			record.Username = username;

			Serialize(record, "record");
		}

		/// <summary>
		/// Overload for Record that allows a Transcription object to be passed.
		/// </summary>
		/// <param name="attempts">How many times the caller can attempt input before an error is thrown.</param>
		/// <param name="bargein">Should the user be allowed to barge in before TTS is complete?</param>
		/// <param name="beep">When set to true, callers will hear a tone indicating the recording has begun.</param>
		/// <param name="choices">The grammar to use in recognizing and validating input.</param>
		/// <param name="format">This specifies the format for the audio recording.</param>
		/// <param name="maxSilence">The maximum amount of time, in seconds, to wait for silence after a user stops speaking.</param>
		/// <param name="maxTime">The maximum amount of time, in seconds, the user is allotted for input.</param>
		/// <param name="method">This defines how you want to send the audio file.</param>
		/// <param name="password">This identifies the FTP account password.</param>
		/// <param name="required">Determines whether Tropo should move on to the next action.</param>
		/// <param name="say">This determines what is played or sent to the caller.</param>
		/// <param name="timeout">The amount of time Tropo will wait, in seconds, after sending or playing the prompt for the user to begin a response.</param>
		/// <param name="transcription">This allows you to submit a recording to be transcribed and specifies where to send the transcription.</param>
		/// <param name="username">This identifies the FTP account username.</param>
		/// <param name="url">This is the destination URL to send the recording.</param>
		public void Record(int? attempts, bool? bargein, bool? beep, Choices choices, string format, float? maxSilence, float? maxTime, string method, string password, bool? required, Say say, float? timeout, Transcription transcription, string username, string url)
		{
			var record = new Record();
			record.Attempts = attempts;
			record.Bargein = bargein;
			record.Beep = beep;
			record.Choices = choices;
			record.Format = format;
			record.MaxSilence = maxSilence;
			record.MaxTime = maxTime;
			record.Method = method;
			record.Password = password;
			record.Required = required;
			record.Say = say;
			record.Timeout = timeout;
			record.Transcription = transcription;
			record.Url = url;
			record.Username = username;

			Serialize(record, "record");
		}

		/// <summary>
		/// Overload for Record that allows events to be set via allowSignals.
		/// </summary>
		/// <param name="attempts"></param>
		/// <param name="allowSignals">Allows for the assignment of an interruptable signal for this Tropo function</param>
		/// <param name="bargein"></param>
		/// <param name="beep"></param>
		/// <param name="choices"></param>
		/// <param name="format"></param>
		/// <param name="maxSilence"></param>
		/// <param name="maxTime"></param>
		/// <param name="method"></param>
		/// <param name="password"></param>
		/// <param name="required"></param>
		/// <param name="say"></param>
		/// <param name="timeout"></param>
		/// <param name="transcription"></param>
		/// <param name="username"></param>
		/// <param name="url"></param>
		public void Record(int? attempts, Array allowSignals, bool? bargein, bool? beep, Choices choices, string format, float? maxSilence, float? maxTime, string method, string password, bool? required, Say say, float? timeout, Transcription transcription, string username, string url)
		{
			var record = new Record();
			record.Attempts = attempts;
			record.allowSignals = allowSignals;
			record.Bargein = bargein;
			record.Beep = beep;
			record.Choices = choices;
			record.Format = format;
			record.MaxSilence = maxSilence;
			record.MaxTime = maxTime;
			record.Method = method;
			record.Password = password;
			record.Required = required;
			record.Say = say;
			record.Timeout = timeout;
			record.Transcription = transcription;
			record.Url = url;
			record.Username = username;

			Serialize(record, "record");
		}

		/// <summary>
		/// Overload for Record that allows a Record object to be passed.
		/// </summary>
		/// <param name="record">A Record object.</param>
		public void Record(Record record)
		{
			Record(record.Attempts, record.Bargein, record.Beep, record.Choices, record.Format, record.MaxSilence, record.MaxTime, record.Method, record.Password, record.Required, record.Say, record.Timeout, record.Username, record.Url);
		}

		#endregion

		#region Recording

		/// <summary>
		/// Allows Tropo applications to begin recording the current session.
		/// The resulting recording may then be sent via FTP or an HTTP POST/Multipart Form. 
		/// </summary>
		/// <param name="format">This specifies the format for the audio recording; it can be 'audio/wav' or 'audio/mp3'.</param>
		/// <param name="method">This defines how you want to send the audio file.</param>
		/// <param name="url">This is the destination URL to send the recording.</param>
		/// <param name="username">This identifies the FTP account username.</param>
		/// <param name="password">This identifies the FTP account password.</param>
		public void StartRecording(string format, string method, string url, string username, string password)
		{
			var startRecording = new StartRecording();
			startRecording.Format = format;
			startRecording.Method = method;
			startRecording.Url = url;
			startRecording.Username = username;
			startRecording.Password = password;

			Serialize(startRecording, "startRecording");
		}

		/// <summary>
		/// Overload for StartRecording that allows a a StartRecording object to be passed directly. 
		/// </summary>
		/// <param name="startRecording">A StartRecording object.</param>
		public void StartRecording(StartRecording startRecording)
		{
			StartRecording(startRecording.Format, startRecording.Method, startRecording.Url, startRecording.Username, startRecording.Password);
		}

		/// <summary>
		/// Stops a previously started recording.
		/// </summary>
		public void StopRecording()
		{
			var stopRecording = new StopRecording();
			Serialize(stopRecording, "stopRecording");
		}

		#endregion

		#region Redirect

		/// <summary>
		/// The redirect function forwards an incoming call to another destination / phone number before answering it.
		/// The redirect function must be called before answer is called; redirect expects that a call be in the ringing or answering state.
		/// Use transfer when working with active answered calls. 
		/// </summary>
		/// <param name="to">The SIP destination for the incoming call, as a URL.</param>
		/// <param name="name">Identifies the return value of a Redirect, so you know the context for the returned information.</param>
		/// <param name="required">Determines whether Tropo should move on to the next action.</param>
		public void Redirect(IEnumerable<string> to, string name, bool? required)
		{
			var redirect = new Redirect();
			redirect.To = to;
			redirect.Name = name;
			redirect.Required = required;

			Serialize(redirect, "redirect");

		}

		/// <summary>
		/// Overload for Redirect that allow a Redirect object to be passed.
		/// </summary>
		/// <param name="redirect">A Redirect object.</param>
		public void Redirect(Redirect redirect)
		{
			Redirect(redirect.To, redirect.Name, redirect.Required);
		}

		/// <summary>
		/// Allows Tropo applications to reject incoming sessions before they are answered. 
		/// </summary>
		public void Reject()
		{
			var reject = new Reject();
			Serialize(reject, "reject");

		}

		#endregion

		#region Say

		/// <summary>
		/// When the current session is a voice channel this key will either play a message or an audio file from a URL.
		/// In the case of an text channel it will send the text back to the user via i nstant messaging or SMS. 
		/// </summary>
		/// <param name="value">This defines what the user will hear when the action is executed. </param>
		/// <param name="as">This specifies the type of data being spoken, so the TTS Engine can interpret it correctly.</param>
		/// <param name="name">Identifies the return value of a Say, so you know the context for the returned information.</param>
		/// <param name="required">Determines whether Tropo should move on to the next action.</param>
		public void Say(string @value, string @as, string name, bool? required)
		{
			var say = new Say();
			say.Value = @value;
			say.As = @as;
			say.Name = name;
			say.Required = required;
			say.Voice = string.IsNullOrEmpty(this.Tropo.Voice) ? null : this.Tropo.Voice;

			Serialize(say, "say");
		}

		/// <summary>
		/// Overload for say that allows events to be set via allowSignals.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="allowSignals">Allows for the assignment of an interruptable signal for this Tropo function</param>
		/// <param name="as"></param>
		/// <param name="name"></param>
		/// <param name="required"></param>
		public void Say(string @value, Array allowSignals, string @as, string name, bool? required)
		{
			var say = new Say();
			say.allowSignals = allowSignals;
			say.Value = @value;
			say.As = @as;
			say.Name = name;
			say.Required = required;
			say.Voice = string.IsNullOrEmpty(this.Tropo.Voice) ? null : this.Tropo.Voice;

			Serialize(say, "say");
		}

		/// <summary>
		/// Overload method for Say that allows only a string value to be passed.
		/// </summary>
		/// <param name="value">The prompt to say or send to the user.</param>
		public void Say(string @value)
		{
			Say(value, null, null, null);
		}

		/// <summary>
		/// Overload method for Say that allows a Say object to be passed directly.
		/// </summary>
		/// <param name="say">A Say object.</param>
		public void Say(Say say)
		{
			Say(say.Value, say.As, say.Name, say.Required);
		}

		/// <summary>
		/// Ooverload method for Say that allows an arrat of prompts to be used.
		/// </summary>
		/// <param name="says">The prompts to say or send to the caller.</param>
		public void Say(IEnumerable<string> says)
		{
			var saysToAdd = new List<Say>();
			foreach (var element in says)
			{
				var say = new Say();
				say.Value = element;
				say.As = null;
				say.Name = null;
				say.Required = true;
				saysToAdd.Add(say);
			}

			var settings = new JsonSerializerSettings();
			settings.DefaultValueHandling = DefaultValueHandling.Ignore;
			this.Tropo.ActionElements.Add("{ \"say\":" + JsonConvert.SerializeObject(saysToAdd, Formatting.None, settings) + "}");

		}

		#endregion

		#region Transfer

		/// <summary>
		/// Transfers an already answered call to another destination / phone number.
		/// Call may be transferred to another phone number or SIP address, which is set through the "to" parameter and is in URL format.
		/// </summary>
		/// <param name="answerOnMedia">If this is set to true, the call will be considered "answered" and audio will begin playing as soon as media is received from the far end.</param>
		/// <param name="choices">The grammar to use in recognizing and validating input.</param>
		/// <param name="from">A string representing who the call is from.</param>
		/// <param name="on">An On object.</param>
		/// <param name="ringRepeat">The number of rings to allow on the outbound call attempt.</param>
		/// <param name="timeout">The amount of time Tropo will wait, in seconds, after sending or playing the prompt for the user to begin a response.</param>
		/// <param name="to">The new destination for the incoming call as a URL.</param>
		public void Transfer(bool? answerOnMedia, Choices choices, string from, On on, float? timeout, IEnumerable<string> to)
		{
			var transfer = new Transfer();
			transfer.AnswerOnMedia = answerOnMedia;
			transfer.Choices = choices;
			transfer.From = from;
			transfer.On = on;
			transfer.Timeout = timeout;
			transfer.To = to;

			Serialize(transfer, "transfer");
		}

		/// <summary>
		/// Overload for Transfer that allows events to be set via allowSignals.
		/// </summary>
		/// <param name="answerOnMedia"></param>
		/// <param name="allowSignals">Allows for the assignment of an interruptable signal for this Tropo function</param>
		/// <param name="choices"></param>
		/// <param name="from"></param>
		/// <param name="on"></param>
		/// <param name="timeout"></param>
		/// <param name="to"></param>
		public void Transfer(bool? answerOnMedia, Array allowSignals, Choices choices, string from, On on, float? timeout, IEnumerable<string> to)
		{
			var transfer = new Transfer();
			transfer.AnswerOnMedia = answerOnMedia;
			transfer.allowSignals = allowSignals;
			transfer.Choices = choices;
			transfer.From = from;
			transfer.On = on;
			transfer.Timeout = timeout;
			transfer.To = to;

			Serialize(transfer, "transfer");
		}

		/// <summary>
		/// Overload for Transfer that allows a Transfer object to be passed directly.
		/// </summary>
		/// <param name="transfer">A Transfer object.</param>
		public void Transfer(Transfer transfer)
		{
			Transfer(transfer.AnswerOnMedia, transfer.Choices, transfer.From, transfer.On, transfer.Timeout, transfer.To);
		}

		#endregion

		/// <summary>
		/// Method to serialize Tropo action objects and add to the base Tropo object.
		/// </summary>
		/// <param name="action"></param>
		/// <param name="prefix"></param>
		private void Serialize(TropoBase action, string prefix)
		{
			var settings = new JsonSerializerSettings
			{
				DefaultValueHandling = DefaultValueHandling.Ignore
			};

			Tropo.ActionElements.Add("{ \"" + prefix + "\":" + JsonConvert.SerializeObject(action, Formatting.None, settings) + "}");
		}

		public string RenderJSON()
		{
			return this.Tropo.RenderJSON();
		}

		#region Static Session

		public static Stream ExecuteMessageRequest(TropoSetting setting, IDictionary<string, string> parameters)
		{
			return ExecuteRequest(setting, setting.MessageToken, parameters);
		}

        public static Task<Stream> ExecuteMessageRequestAsync(TropoSetting setting, IDictionary<string, string> parameters)
        {
            return ExecuteRequestAsync(setting, setting.MessageToken, parameters);
        }

		public static Stream ExecuteVoiceRequest(TropoSetting setting, IDictionary<string, string> parameters)
		{
			return ExecuteRequest(setting, setting.VoiceToken, parameters);
		}

		private static Stream ExecuteRequest(TropoSetting setting, string token, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			// Format the session initiation URL.
			var endpoint = $"{setting.ApiUrl}/{setting.ApiVersion}/sessions?action=create&token={token}";

			// Interate over parameters and append to endpoint URL.
			if (parameters != null)
			{
				endpoint = parameters.Aggregate(endpoint, (current, param) => current + $"&{param.Key}={param.Value}");
			}

			// Set up HTTP request.
			var request = (HttpWebRequest)WebRequest.Create(endpoint);
			request.Method = "GET";

			// Get the response.
			var response = (HttpWebResponse)request.GetResponse();

			return response.GetResponseStream();
		}

        private static async Task<Stream> ExecuteRequestAsync(TropoSetting setting, string token, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            // Format the session initiation URL.
            var endpoint = $"{setting.ApiUrl}/{setting.ApiVersion}/sessions?action=create&token={token}";

            // Interate over parameters and append to endpoint URL.
            if (parameters != null)
            {
                endpoint = parameters.Aggregate(endpoint, (current, param) => current + $"&{param.Key}={param.Value}");
            }

            // Set up HTTP request.
            var request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "GET";

            // Get the response.
            var response = (HttpWebResponse)(await request.GetResponseAsync());

            return response.GetResponseStream();
        }

		#endregion
	}
}