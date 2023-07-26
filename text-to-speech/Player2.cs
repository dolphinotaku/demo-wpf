using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace QMS_Test
{
    //System.Media.SoundPlayer
    public class Player2
    {
        public void PlaySound(FileInfo[] FileInfos)
        {
            foreach (var fi in FileInfos)
            {
                System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(fi.FullName);
                sndPlayer.Load();
                sndPlayer.PlaySync();
            }
        }

        public void PlaySound2(FileInfo[] FileInfos)
        {
            List<SoundPlayer> list = new List<SoundPlayer>();
            foreach (var fi in FileInfos)
            {
                SoundPlayer sndPlayer = new System.Media.SoundPlayer(fi.FullName);
                sndPlayer.Load();
                list.Add(sndPlayer);
            }

            foreach (var player in list)
            {
                player.PlaySync();
                player.Dispose();
            }

        }

        public void PlaySound3(FileInfo[] FileInfos)
        {
            List<SoundPlayer> list = new List<SoundPlayer>();
            try
            {
                foreach (var fi in FileInfos)
                {
                    SoundPlayer sndPlayer = new System.Media.SoundPlayer(fi.FullName);
                    sndPlayer.Load();
                    list.Add(sndPlayer);
                }

                foreach (var player in list)
                {
                    player.PlaySync();
                }

            }
            finally
            {
                foreach (var player in list)
                {
                    player.Dispose();
                }
            }
        }

        public void PlayAsync(FileInfo[] FileInfos)
        {
            var t = Task.Run(() => PlaySound3(FileInfos));
            //t.Wait();
        }

    }
}
