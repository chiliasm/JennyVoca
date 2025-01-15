using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class SoundManager : BaseMonoSingleton<SoundManager>
    {
        public enum E_Sound_Channel
        {
            None = -1,

            Bgm,
            Sfx,

            Max,
        }

        #region // [Var] Data //
        const int MAX_SOUND_CHANNEL_BGM = 1;
        const int MAX_SOUND_CHANNEL_SFX = 7;
        const int MAX_SOUND_CACHING_COUNT = 50;
        const string PATH_SOUND_BGM = "Sound/Bgm";
        const string PATH_SOUND_SFX = "Sound/Sfx";

        readonly AudioSource[] mBgmList = new AudioSource[MAX_SOUND_CHANNEL_BGM];
        readonly AudioSource[] mSfxList = new AudioSource[MAX_SOUND_CHANNEL_SFX];

        readonly Dictionary<E_Sound_Item, AudioClip> mCachingDic = new();
        readonly Dictionary<E_Sound_Item, long> mCachingTimeStampDic = new();
        #endregion


        #region // [Func] Unity //
        protected override void OnDisable()
        {
            base.OnDisable();

            StopAll();
        }
        #endregion

        #region // [Func] Singleton //
        public override void OnCreateSingleton()
        {
            base.OnCreateSingleton();

            for (int i = 0; i < MAX_SOUND_CHANNEL_BGM; i++)
            {
                var comp = Go.AddComponent<AudioSource>();
                comp.loop = true;
                comp.dopplerLevel = 0f;
                comp.reverbZoneMix = 0f;
                mBgmList[i] = comp;
            }
            for (int i = 0; i < MAX_SOUND_CHANNEL_SFX; i++)
            {
                var comp = Go.AddComponent<AudioSource>();
                comp.dopplerLevel = 0f;
                comp.reverbZoneMix = 0f;
                mSfxList[i] = comp;
            }
        }
        #endregion

        #region // [Func] Play //
        public int Play(E_Sound_Item item, float volume = 1f)
        {
            var channel = GetSoundChannel(item);
            var emptyChannel = GetEmptyChannel(channel);
            if (emptyChannel >= 0)
            {
                switch (channel)
                {
                    case E_Sound_Channel.Bgm:
                        {
                            var audioClip = GetOrNewAudioClip(item);
                            if (audioClip != null)
                            {
                                mBgmList[emptyChannel].clip = audioClip;
                                mBgmList[emptyChannel].volume = volume;
                                mBgmList[emptyChannel].Play();
                            }
                        }
                        break;
                    case E_Sound_Channel.Sfx:
                        {
                            var audioClip = GetOrNewAudioClip(item);
                            if (audioClip != null)
                            {
                                mSfxList[emptyChannel].PlayOneShot(audioClip, volume);
                            }
                        }
                        break;
                }
            }
            return emptyChannel;
        }

        public void Stop(E_Sound_Channel channel)
        {
            switch (channel)
            {
                case E_Sound_Channel.Bgm:
                    {
                        foreach (var it in mBgmList)
                        {
                            if (it != null)
                                it.Stop();
                        }   
                    }
                    break;
                case E_Sound_Channel.Sfx:
                    {
                        foreach (var it in mSfxList)
                        {
                            if (it != null)
                                it.Stop();
                        }   
                    }
                    break;
            }
        }

        public void StopAll()
        {
            for (E_Sound_Channel i = E_Sound_Channel.Bgm; i < E_Sound_Channel.Max; i++)
                Stop(i);
        }
        #endregion

        #region // [Func] Channel //
        int GetEmptyChannel(E_Sound_Channel channel)
        {
            int emptyChannel = -1;
            switch (channel)
            {
                case E_Sound_Channel.Bgm:
                    {
                        for (int i = 0; i < mBgmList.Length; i++)
                        {
                            if (mBgmList[i].isPlaying == false)
                            {
                                emptyChannel = i;
                                break;
                            }
                        }
                        if (emptyChannel == -1 && mBgmList.Length > 0)
                            emptyChannel = 0;
                    }
                    break;
                case E_Sound_Channel.Sfx:
                    {
                        for (int i = 0; i < mSfxList.Length; i++)
                        {
                            if (mSfxList[i].isPlaying == false)
                            {
                                emptyChannel = i;
                                break;
                            }
                        }
                        if (emptyChannel == -1 && mSfxList.Length > 0)
                            emptyChannel = 0;
                    }
                    break;
            }
            return emptyChannel;
        }
        #endregion

        #region // [Func] Load //
        AudioClip GetOrNewAudioClip(E_Sound_Item item)
        {
            if (item == E_Sound_Item.None)
                return null;

            if (mCachingDic.TryGetValue(item, out var audioClip) == false)
            {
                if (mCachingTimeStampDic.Count >= MAX_SOUND_CACHING_COUNT)
                {
                    var earlyKey = GetEarlyAudioClip();
                    if (earlyKey != E_Sound_Item.None)
                    {
                        if (mCachingTimeStampDic.Remove(earlyKey))
                        {
                            if (mCachingDic.Remove(earlyKey))
                                Resources.UnloadUnusedAssets();
                        }
                    }
                }

                var path = GetSoundPath(item);
                if (string.IsNullOrEmpty(path) == false)
                {
                    audioClip = ResourceManager.Instance.Load(path) as AudioClip;
                    mCachingDic[item] = audioClip;
                    mCachingTimeStampDic[item] = System.DateTime.Now.Ticks;
                }   
            }
            return audioClip;
        }
        #endregion

        #region // [Func] Util //
        E_Sound_Channel GetSoundChannel(E_Sound_Item item)
        {
            var type = E_Sound_Channel.None;
            if (item > E_Sound_Item.Bgm_Begin && item < E_Sound_Item.Bgm_End)
                type = E_Sound_Channel.Bgm;
            else if (item > E_Sound_Item.Sfx_Begin && item < E_Sound_Item.Sfx_End)
                type = E_Sound_Channel.Sfx;
            return type;
        }

        string GetSoundPath(E_Sound_Item item)
        {
            var channel = GetSoundChannel(item);
            var name = channel switch
            {
                E_Sound_Channel.Bgm => PATH_SOUND_BGM,
                E_Sound_Channel.Sfx => PATH_SOUND_SFX,
                _ => string.Empty
            };
            if (string.IsNullOrEmpty(name))
                return name;
            return string.Format("{0}/{1}", name, item.ToString());
        }

        E_Sound_Item GetEarlyAudioClip()
        {
            var earlyKey = E_Sound_Item.None;
            var earlyTime = long.MaxValue;
            foreach (var it in mCachingTimeStampDic)
            {
                if (it.Value < earlyTime)
                {
                    earlyKey = it.Key;
                    earlyTime = it.Value;
                }
            }
            return earlyKey;
        }

        #endregion
    }
}
