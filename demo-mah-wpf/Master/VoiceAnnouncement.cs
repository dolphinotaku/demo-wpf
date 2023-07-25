using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf.Master
{
    public class VoiceAnnouncement
    {
        protected SpeakLanguage _speakLanguage;
        protected int _speakRate; // speed of voice
        protected string _eng_text;
        protected string _zh_hk_text;
        protected string _zh_cn_text;
        protected string _ticketNum;
        protected string _roomNum;

        public SpeakLanguage SpeakLanguage
        {
            get
            {
                return this._speakLanguage;
            }
            set
            {
                this._speakLanguage = value;
            }
        }
        public int SpeakRate
        {
            get
            {
                return this._speakRate;
            }
            set
            {
                this._speakRate = value;
            }
        }
        public string Eng_Text
        {
            get
            {
                return this._eng_text;
            }
            set
            {
                this._eng_text = value;
            }
        }
        public string ZH_HK_Text
        {
            get
            {
                return this._zh_hk_text;
            }
            set
            {
                this._zh_hk_text = value;
            }
        }
        public string ZH_CN_Text
        {
            get
            {
                return this._zh_cn_text;
            }
            set
            {
                this._zh_cn_text = value;
            }
        }

        public string TicketNum
        {
            get { return _ticketNum; }
            set
            {
                _ticketNum = value;
            }
        }
        public string RoomNum
        {
            get { return _roomNum; }
            set
            {
                _roomNum = value;
            }
        }
        public VoiceAnnouncement()
        {
            this.SpeakLanguage = SpeakLanguage.English;
            this._eng_text = "Testing 1 2 3, testing completed.";
            this.SpeakRate = 1;
        }
        public VoiceAnnouncement(SpeakLanguage speakLanguage, string _ticketNum, string _roomNum) : base()
        {
            this.SpeakLanguage = speakLanguage;
            this.TicketNum = _ticketNum;
            this.RoomNum = _roomNum;
            this.SpeakRate = 1;
        }
        public VoiceAnnouncement(SpeakLanguage speakLanguage, string _ticketNum, string _roomNum, int speakRate) : base()
        {
            this.SpeakLanguage = speakLanguage;
            this.TicketNum = _ticketNum;
            this.RoomNum = _roomNum;
            this.SpeakRate = speakRate;
        }
        public string SpeakLanguageToString()
        {
            return this.SpeakLanguageToString(this.SpeakLanguage);
        }
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
    }

    public enum SpeakLanguage
    {
        English,
        ChineseTraditional,
        ChineseSimplified
    }
}
