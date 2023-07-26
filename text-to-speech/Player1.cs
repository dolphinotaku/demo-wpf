using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS_Test
{
    //COM component: Microsoft speech object Library
    public class Player1
    {
        public void PlaySound(FileInfo[] FileInfos)
        {
            foreach (var fi in FileInfos)
            {
                SpeechLib.SpVoice pp = new SpeechLib.SpVoice();
                SpeechLib.SpFileStream spFs = new SpeechLib.SpFileStream();

                spFs.Open(fi.FullName, SpeechLib.SpeechStreamFileMode.SSFMOpenForRead, true);
                SpeechLib.ISpeechBaseStream Istream = spFs as SpeechLib.ISpeechBaseStream;
                pp.SpeakStream(Istream, SpeechLib.SpeechVoiceSpeakFlags.SVSFIsFilename);
                spFs.Close();
            }
        }
    }
}
