using demo_mah_wpf.Master;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf.Service
{
    public class VoiceService : IVoiceService
    {
        protected readonly System.Timers.Timer announceTimer;
        protected readonly int defaultDataTimeInterval = 5;

        private List<VoiceAnnouncement> announcementQueue = new List<VoiceAnnouncement>();
        public VoiceAnnouncement currentAnnouncement = new VoiceAnnouncement();

        private SpeechSynthesizer speaker = new SpeechSynthesizer();
        private volatile bool _isCurrentlySpeaking = false;

        /// <summary>Event handler. Fired when the SpeechSynthesizer object starts speaking asynchronously.</summary>
        private void StartedSpeaking(object sender, SpeakStartedEventArgs e)
        { this.GetInstance()._isCurrentlySpeaking = true; }
        /// <summary>Event handler. Fired when the SpeechSynthesizer object finishes speaking asynchronously.</summary>
        private void FinishedSpeaking(object sender, SpeakCompletedEventArgs e)
        { this.GetInstance()._isCurrentlySpeaking = false; }
        /// <summary>Event handler. Fired when the SpeechSynthesizer object starts speaking asynchronously.</summary>
        private void StartedSpeaking_2()
        { this.GetInstance()._isCurrentlySpeaking = true; }
        /// <summary>Event handler. Fired when the SpeechSynthesizer object finishes speaking asynchronously.</summary>
        private void FinishedSpeaking_2()
        { this.GetInstance()._isCurrentlySpeaking = false; }

        protected string Eng_Cubicle_Template;
        protected string ZH_HK_Cubicle_Template;
        protected string ZH_CN_Cubicle_Template;

        protected string Eng_Staff_Template;
        protected string ZH_HK_Staff_Template;
        protected string ZH_CN_Staff_Template;

        public event EventHandler OnSomeEvent = delegate { };
        public bool IsCurrentlySpeaking
        {
            get { return this._isCurrentlySpeaking; }
        }

        private static readonly Lazy<VoiceService> lazyAnnounceService = new Lazy<VoiceService>(() => new VoiceService());

        public VoiceService()
        {
            Log.Debug("VoiceService on");

            this.announceTimer = new System.Timers.Timer(this.defaultDataTimeInterval);
            announceTimer.Elapsed += PlayVoiceTimerTick;
            announceTimer.Start();

            //this.announceTimer = new DispatcherTimer();
            //announceTimer.Interval = TimeSpan.FromSeconds(this.defaultDataTimeInterval);
            //announceTimer.Tick += PlayVoiceTimerTick;
            //announceTimer.Start();

            this.speaker.SpeakStarted += new EventHandler<SpeakStartedEventArgs>(StartedSpeaking);
            this.speaker.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(FinishedSpeaking);

            this.Eng_Cubicle_Template = "Hello Ticket number {0} please go to zone {1} at the outdoor area";
            this.ZH_HK_Cubicle_Template = "你好醜号 {0} 請到 {1}";
            this.ZH_CN_Cubicle_Template = "你好筹號 {0} 请到 {1}";

            this.Eng_Staff_Template = "Hello Ticket number {0} please go to zone {1} at the indoor area";
            this.ZH_HK_Staff_Template = "你好醜号 {0} 請到 {1}";
            this.ZH_CN_Staff_Template = "你好筹號 {0} 请到 {1}";
        }
        private async void PlayVoiceTimerTick(object sender, EventArgs args)
        {
            var msg = string.Format("PlayVoiceTimerTick() - execute - every {0} seconds", this.defaultDataTimeInterval);
            Log.Debug(msg);

            var _announcementQueueList = this.GetInstance().announcementQueue;
            if (_announcementQueueList == null || _announcementQueueList.Count <= 0) return;
            if (this.GetInstance()._isCurrentlySpeaking) return;


            // use task to wrap SpeechSynthesizer
            // force it running in async, otherwise the screen will freeze when speak
            var task = Task.Factory.StartNew(async () =>
            {
                Log.Debug(string.Format("PlayVoiceTimerTick() - task - start"));
                SpeechSynthesizer speech = this.GetInstance().speaker;
                this.StartedSpeaking_2();

                //VoiceAnnouncement _currentAnnouncement = _announcementQueueList[0];
                //do
                //{
                Log.Debug(string.Format("PlayVoiceTimerTick() - task - get index 0 announcement"));
                VoiceAnnouncement _currentAnnouncement = _announcementQueueList[0];
                this.currentAnnouncement = _currentAnnouncement;
                // remove element after play voice
                _announcementQueueList.RemoveAt(0);

                speech.Rate = 1;

                //Thread t = new Thread(new ThreadStart(()=> PlayVoices(speech, _currentAnnouncement)));
                //t.Start();

                //Task playEngTask = this.PlayVoices(speech, _currentAnnouncement);
                //Prompt toast = Task.FromResult(playEngTask.Result);

                //SpeechSynthesizer synth = new SpeechSynthesizer();
                //synth.SelectVoice(this.SpeakLanguageToString(SpeakLanguage.English));
                //synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Senior);
                //synth.SpeakAsync("Every night in my dreams");

                //await this.PlayVoices(speech, _currentAnnouncement);
                //this.PlayVoices2(speech, _currentAnnouncement);

                speech.Rate = _currentAnnouncement.SpeakRate;
                speech.SelectVoice(this.SpeakLanguageToString(SpeakLanguage.ChineseTraditional));
                speech.Speak(_currentAnnouncement.ZH_HK_Text);

                speech.Rate = _currentAnnouncement.SpeakRate;
                speech.SelectVoice(this.SpeakLanguageToString(SpeakLanguage.English));
                speech.Speak(_currentAnnouncement.Eng_Text);

                speech.Rate = _currentAnnouncement.SpeakRate;
                speech.SelectVoice(this.SpeakLanguageToString(SpeakLanguage.ChineseSimplified));
                speech.Speak(_currentAnnouncement.ZH_CN_Text);

                this.FinishedSpeaking_2();
                //} while (_announcementQueueList.Count>0);
            });
            await task;
        }

        private async Task<bool> PlayVoices(SpeechSynthesizer speech, VoiceAnnouncement _currentAnnouncement)
        {
            await this.PlayZH_HKVoice(speech, _currentAnnouncement);
            //await Task.Delay(2000);
            await this.PlayEngVoice(speech, _currentAnnouncement);
            //await Task.Delay(2000);
            await this.PlayZH_CNVoice(speech, _currentAnnouncement);

            //return Task.FromResult<bool>(true);
            return true;
        }
        private void PlayVoices2(SpeechSynthesizer speech, VoiceAnnouncement _currentAnnouncement)
        {
            this.PlayZH_HKVoice2(speech, _currentAnnouncement);
            //await Task.Delay(2000);
            this.PlayEngVoice2(speech, _currentAnnouncement);
            //await Task.Delay(2000);
            this.PlayZH_CNVoice2(speech, _currentAnnouncement);
        }

        private Task<Prompt> PlayZH_HKVoice(SpeechSynthesizer speech, VoiceAnnouncement _currentAnnouncement)
        {
            speech.Rate = _currentAnnouncement.SpeakRate;
            speech.SelectVoice(this.SpeakLanguageToString(SpeakLanguage.ChineseTraditional));
            //speech.Speak(_currentAnnouncement.ZH_HK_Text);
            //await speech.SpeakAsync(_currentAnnouncement.ZH_HK_Text);
            return Task.FromResult<Prompt>(speech.SpeakAsync(_currentAnnouncement.ZH_HK_Text));
        }
        private Task<Prompt> PlayEngVoice(SpeechSynthesizer speech, VoiceAnnouncement _currentAnnouncement)
        {
            speech.Rate = _currentAnnouncement.SpeakRate;
            speech.SelectVoice(this.SpeakLanguageToString(SpeakLanguage.English));
            //speech.Speak(_currentAnnouncement.Eng_Text);
            return Task.FromResult<Prompt>(speech.SpeakAsync(_currentAnnouncement.Eng_Text));
        }
        private Task<Prompt> PlayZH_CNVoice(SpeechSynthesizer speech, VoiceAnnouncement _currentAnnouncement)
        {
            speech.Rate = _currentAnnouncement.SpeakRate;
            speech.SelectVoice(this.SpeakLanguageToString(SpeakLanguage.ChineseSimplified));
            //speech.Speak(_currentAnnouncement.ZH_CN_Text);
            return Task.FromResult<Prompt>(speech.SpeakAsync(_currentAnnouncement.ZH_CN_Text));
        }


        private void PlayZH_HKVoice2(SpeechSynthesizer speech, VoiceAnnouncement _currentAnnouncement)
        {
            speech.Rate = _currentAnnouncement.SpeakRate;
            speech.SelectVoice(this.SpeakLanguageToString(SpeakLanguage.ChineseTraditional));
            speech.Speak(_currentAnnouncement.ZH_HK_Text);
        }
        private void PlayEngVoice2(SpeechSynthesizer speech, VoiceAnnouncement _currentAnnouncement)
        {
            speech.Rate = _currentAnnouncement.SpeakRate;
            speech.SelectVoice(this.SpeakLanguageToString(SpeakLanguage.English));
            speech.Speak(_currentAnnouncement.Eng_Text);
        }
        private void PlayZH_CNVoice2(SpeechSynthesizer speech, VoiceAnnouncement _currentAnnouncement)
        {
            speech.Rate = _currentAnnouncement.SpeakRate;
            speech.SelectVoice(this.SpeakLanguageToString(SpeakLanguage.ChineseSimplified));
            speech.Speak(_currentAnnouncement.ZH_CN_Text);
        }

        public List<VoiceAnnouncement> GetAnnouncementList()
        {
            var _announcementQueueList = this.GetInstance().announcementQueue;
            return _announcementQueueList;
        }
        public void SetAnnouncementList(List<VoiceAnnouncement> list)
        {
            var _announcementQueueList = this.GetInstance().announcementQueue;
            _announcementQueueList = list;
        }
        public void Add(VoiceAnnouncement voiceAnnouncement)
        {
            var _announcementQueueList = this.GetInstance().announcementQueue;
            this.MergeSpeechOfVoice(voiceAnnouncement);
            //_announcementQueueList.Add(value);
            _announcementQueueList.Insert(0, voiceAnnouncement);
        }
        public void AddRange(List<VoiceAnnouncement> list)
        {
            var _announcementQueueList = this.GetInstance().announcementQueue;
            foreach (VoiceAnnouncement voiceAnnouncement in list)
            {
                this.MergeSpeechOfVoice(voiceAnnouncement);
                _announcementQueueList.Insert(0, voiceAnnouncement);
            }

            //this.GetInstance().announcementQueue.AddRange(list);
        }

        /// <summary>
        /// this method will merge voice according to the tick number and room number
        /// </summary>
        public void MergeSpeechOfVoice(VoiceAnnouncement _voiceAnnouncement)
        {
            if (_voiceAnnouncement.RoomNum.StartsWith("S"))
            {
                _voiceAnnouncement.Eng_Text = string.Format(this.Eng_Staff_Template, _voiceAnnouncement.TicketNum, _voiceAnnouncement.RoomNum);
                _voiceAnnouncement.ZH_HK_Text = string.Format(this.ZH_HK_Staff_Template, _voiceAnnouncement.TicketNum, _voiceAnnouncement.RoomNum);
                _voiceAnnouncement.ZH_CN_Text = string.Format(this.ZH_CN_Staff_Template, _voiceAnnouncement.TicketNum, _voiceAnnouncement.RoomNum);
            }
            else
            {
                _voiceAnnouncement.Eng_Text = string.Format(this.Eng_Cubicle_Template, _voiceAnnouncement.TicketNum, _voiceAnnouncement.RoomNum);
                _voiceAnnouncement.ZH_HK_Text = string.Format(this.ZH_HK_Cubicle_Template, _voiceAnnouncement.TicketNum, _voiceAnnouncement.RoomNum);
                _voiceAnnouncement.ZH_CN_Text = string.Format(this.ZH_CN_Cubicle_Template, _voiceAnnouncement.TicketNum, _voiceAnnouncement.RoomNum);
            }
        }

        //public string SpeakLanguageToString()
        //{
        //    return this.SpeakLanguageToString(this.SpeakLanguage);
        //}
        public string SpeakLanguageToString(SpeakLanguage speakLanguage)
        {
            string _languageName = string.Empty;
            switch (speakLanguage)
            {
                case SpeakLanguage.English: _languageName = "Microsoft Zira Desktop"; break;
                case SpeakLanguage.ChineseTraditional: _languageName = "Microsoft Tracy Desktop"; break;
                case SpeakLanguage.ChineseSimplified: _languageName = "Microsoft Huihui Desktop"; break;
            }
            return _languageName;
        }
        public VoiceService GetInstance()
        {
            return lazyAnnounceService.Value;
        }
    }
}
