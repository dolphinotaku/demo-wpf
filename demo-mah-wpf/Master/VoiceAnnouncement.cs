namespace demo_mah_wpf.Master
{
    public class VoiceAnnouncement
    {
        public int Id { get; set; }
        public SpeakLanguage SpeakLanguage { get; set; }
        public int SpeakRate { get; set; }
        public string EngText { get; set; }
        public string ZhHkText { get; set; }
        public string ZhCnText { get; set; }

        public string TicketNum { get; set; }
        public string RoomNum { get; set; }
        public VoiceAnnouncement()
        {
            SpeakLanguage = SpeakLanguage.English;
            EngText = "Testing 1 2 3, testing completed.";
            SpeakRate = 1;
        }
        public VoiceAnnouncement(int id, SpeakLanguage speakLanguage, string ticketNum, string roomNum)
        {
            Id = id;
            SpeakLanguage = speakLanguage;
            TicketNum = ticketNum;
            RoomNum = roomNum;
            SpeakRate = 1;
        }
        public VoiceAnnouncement(int id, SpeakLanguage speakLanguage, string ticketNum, string roomNum, int speakRate)
        {
            Id = id;
            SpeakLanguage = speakLanguage;
            TicketNum = ticketNum;
            RoomNum = roomNum;
            SpeakRate = speakRate;
        }
        public string SpeakLanguageToString()
        {
            return this.SpeakLanguageToString(this.SpeakLanguage);
        }
        public string SpeakLanguageToString(SpeakLanguage speakLanguage)
        {
            string languageName = string.Empty;
            switch (speakLanguage)
            {
                case SpeakLanguage.English: languageName = "Microsoft Zira Desktop"; break;
                case SpeakLanguage.ChineseTraditional: languageName = "Microsoft Tracy Desktop"; break;
                case SpeakLanguage.ChineseSimplified: languageName = "Microsoft Huihui Desktop"; break;
            }
            return languageName;
        }
    }

    public enum SpeakLanguage
    {
        English,
        ChineseTraditional,
        ChineseSimplified
    }
}
