using demo_mah_wpf.Master;
using System;
using System.Collections.Generic;

namespace demo_mah_wpf.Service
{
    public interface IVoiceService
    {
        event EventHandler OnSomeEvent;
        public List<VoiceAnnouncement> GetAnnouncementList();
        public void SetAnnouncementList(List<VoiceAnnouncement> list);
        public void Add(VoiceAnnouncement value);
        public void AddRange(List<VoiceAnnouncement> list);
    }
}
