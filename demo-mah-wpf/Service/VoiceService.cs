using demo_mah_wpf.Master;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;

namespace demo_mah_wpf.Service
{
    public class VoiceService : IVoiceService
    {
        private System.Timers.Timer _announceTimer;
        private readonly int _defaultDataTimeInterval = 1000;

        private List<VoiceAnnouncement> _announcementQueueList;
        private List<VoiceAnnouncement> _announcedRecordList;

        public event EventHandler OnSomeEvent = delegate { };
        public bool IsCurrentlySpeaking { get; set; }

        public VoiceService()
        {
            Log.Debug("VoiceService on");

            _announcementQueueList = new List<VoiceAnnouncement>();
            _announcedRecordList = new List<VoiceAnnouncement>();

            _announceTimer = new System.Timers.Timer(_defaultDataTimeInterval);
            _announceTimer.Elapsed += PlayVoiceTimerTick;
            _announceTimer.Start();
        }
        private void PlayVoiceTimerTick(object sender, EventArgs args)
        {
            _announceTimer.Elapsed -= PlayVoiceTimerTick;
            var msg = $"PlayVoiceTimerTick() - execute - every {_defaultDataTimeInterval} seconds";
            Log.Information(msg);

            if (_announcementQueueList.Count <= 0)
            {
                _announceTimer.Elapsed += PlayVoiceTimerTick;
                return;
            }

            if (IsCurrentlySpeaking)
            {
                _announceTimer.Elapsed += PlayVoiceTimerTick;
                return;
            }

            Log.Debug(string.Format("PlayVoiceTimerTick() - task - start"));
            IsCurrentlySpeaking = true;

            Log.Debug(string.Format("PlayVoiceTimerTick() - task - get index 0 announcement"));

            var currentAnnouncement = _announcementQueueList[0];
            this._announcedRecordList.Add(currentAnnouncement);
            _announcementQueueList.RemoveAt(0);

            Play(currentAnnouncement.SpeakRate, SpeakLanguageToString(SpeakLanguage.English), currentAnnouncement.EngText);
            //Play(currentAnnouncement.SpeakRate, SpeakLanguageToString(SpeakLanguage.English), currentAnnouncement.EngText);
            //Play(currentAnnouncement.SpeakRate, SpeakLanguageToString(SpeakLanguage.English), currentAnnouncement.EngText);
            Play(currentAnnouncement.SpeakRate, SpeakLanguageToString(SpeakLanguage.ChineseTraditional), currentAnnouncement.ZhHkText);
            Play(currentAnnouncement.SpeakRate, SpeakLanguageToString(SpeakLanguage.ChineseSimplified), currentAnnouncement.ZhCnText);

            IsCurrentlySpeaking = false;

            _announceTimer.Elapsed += PlayVoiceTimerTick;
        }

        private void Play(int rate, string voice, string text)
        {
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.SpeakStarted += (s, e) => IsCurrentlySpeaking = true;
            speechSynthesizer.SpeakCompleted += (s, e) => IsCurrentlySpeaking = false;
            speechSynthesizer.Rate = rate;
            speechSynthesizer.SelectVoice(voice);
            speechSynthesizer.Speak(text);
        }

        public List<VoiceAnnouncement> GetAnnouncementList()
        {
            return _announcementQueueList;
        }
        public void SetAnnouncementList(List<VoiceAnnouncement> list)
        {
            _announcementQueueList = list;
        }
        public void Add(VoiceAnnouncement voiceAnnouncement)
        {
            if (this._announcedRecordList.Any(a => a.Id == voiceAnnouncement.Id)) return;
            MergeSpeechOfVoice(voiceAnnouncement);
            _announcementQueueList.Add(voiceAnnouncement);
        }
        public void AddRange(List<VoiceAnnouncement> list)
        {
            foreach (VoiceAnnouncement voiceAnnouncement in list)
            {
                if (this._announcedRecordList.Any(a => a.Id == voiceAnnouncement.Id)) continue;
                MergeSpeechOfVoice(voiceAnnouncement);
                _announcementQueueList.Add(voiceAnnouncement);
            }
        }

        /// <summary>
        /// this method will merge voice according to the tick number and room number
        /// </summary>
        private void MergeSpeechOfVoice(VoiceAnnouncement _voiceAnnouncement)
        {
            if (_voiceAnnouncement.RoomNum.StartsWith("S"))
            {
                _voiceAnnouncement.EngText = $"Hello Ticket number {_voiceAnnouncement.TicketNum} please go to zone {_voiceAnnouncement.RoomNum}";
                _voiceAnnouncement.ZhHkText = $"你好醜号 {_voiceAnnouncement.TicketNum} 請到 {_voiceAnnouncement.RoomNum}";
                _voiceAnnouncement.ZhCnText = $"你好筹號 {_voiceAnnouncement.TicketNum} 请到 {_voiceAnnouncement.RoomNum}";
            }
            else
            {
                _voiceAnnouncement.EngText = $"Hello Ticket number {_voiceAnnouncement.TicketNum} please go to zone {_voiceAnnouncement.RoomNum}";
                _voiceAnnouncement.ZhHkText = $"你好醜号 {_voiceAnnouncement.TicketNum} 請到 {_voiceAnnouncement.RoomNum}";
                _voiceAnnouncement.ZhCnText = $"你好筹號 {_voiceAnnouncement.TicketNum} 请到 {_voiceAnnouncement.RoomNum}";
            }
        }
        private string SpeakLanguageToString(SpeakLanguage speakLanguage)
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
    }
}
