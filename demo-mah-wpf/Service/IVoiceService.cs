using demo_mah_wpf.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf.Service
{
    public interface IVoiceService
    {
        event EventHandler OnSomeEvent;
        public List<VoiceAnnouncement> GetAnnouncementList();
        public void SetAnnouncementList(List<VoiceAnnouncement> list);
        public void Add(VoiceAnnouncement value);
        public void AddRange(List<VoiceAnnouncement> list);
        public VoiceService GetInstance();
    }
}
