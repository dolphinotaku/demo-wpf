using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;

namespace QMS_Test
{
    //add assemblies System.Speech first;
    public class Player3
    {
        //Installed Voices
        //[0] = "Microsoft Hanhan Desktop" ["Enabled"]  Chinese Simplified
        //[1] = "Microsoft Zira Desktop" ["Enabled"]    English USA
        //[2] = "Microsoft Huihui Desktop" ["Enabled"]  Chinese Simplified
        //[3] = "Microsoft Tracy Desktop" ["Enabled"]   Chinese Cantonese
        //[4] = "Microsoft David Desktop" ["Enabled"]   English

        public void PlaySoundZH(string text)
        {
            PlaySound(text, "Microsoft Huihui Desktop");
        }

        public void PlaySoundCH(string text)
        {
            PlaySound(text, "Microsoft Tracy Desktop");
        }
        public void PlaySoundEN(string text)
        {
            PlaySound(text, "Microsoft Zira Desktop");
        }

        public void PlaySound(string text, string voice)
        {
            //List<InstalledVoice> voices = speech.GetInstalledVoices().ToList();
            //speech.SelectVoice(voices[3].VoiceInfo.Name);

            SpeechSynthesizer speech = new SpeechSynthesizer();
            //speech.Volume = value;
            //speech.SpeakCompleted += speech_SpeakCompleted;
            speech.SelectVoice(voice);
            speech.Rate = -3;

            //var audioFormat = new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Mono);
            //speech.SetOutputToWaveFile($"C:\\temp\\{voice}.wav", audioFormat);

            speech.Speak(text);
            //speech.SpeakAsync(text);

            //speech.SetOutputToDefaultAudioDevice();
            speech.Dispose();
        }


    }
}
