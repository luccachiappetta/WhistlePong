using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WhistleDetector : MonoBehaviour
{
    [SerializeField] private float pitchValue;
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private float divisionFactor = 1600;
    
    private float[] samples;
    private float[] spectrum;
    private float sampleRate;
    private const int SAMPLE_SIZE = 1024;
    
    private AudioSource source;
    public float pitchMap;
    

    // Start is called before the first frame update
    void Start()
    {
        samples = new float[SAMPLE_SIZE];
        spectrum = new float[SAMPLE_SIZE];
        sampleRate = AudioSettings.outputSampleRate;
        
        //assign mic to audio clip
        source = GetComponent<AudioSource>();
        source.clip = Microphone.Start(Microphone.devices[0], true, 10, AudioSettings.outputSampleRate);
        source.loop = true;
        source.outputAudioMixerGroup = mixer;
        
        while(!(Microphone.GetPosition(null) > 0)){}
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        AnalyzeSound();
        // pitchMap = Remap(pitchValue, 400f, 0f, 1600f, 1f);
        // pitchMap = Mathf.Clamp(pitchMap, 0f, 1f);

        pitchMap = map01(pitchValue, 400f, 1300f);
        // pitchMap = pitchValue / divisionFactor;
        // Debug.Log(pitchMap);
    }
    
    public void AnalyzeSound()
    {
        source.GetOutputData(samples, 0);

        //get sound spectrum
        source.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        
        float maxV = 0;
        var maxN = 0;
        for (int i = 0; i < SAMPLE_SIZE; i++)
        {
            if(!(spectrum[i] > maxV) || !(spectrum[i] > 0.0f))
                continue;
        
            maxV = spectrum[i];
            maxN = i;
        }
        
        float freqN = maxN;
        if (maxN > 0 && maxN < SAMPLE_SIZE - 1)
        {
            var dL = spectrum[maxN - 1] / spectrum[maxN];
            var dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        
        pitchValue = freqN * (sampleRate / 2) / SAMPLE_SIZE;
        // Debug.Log(pitchValue);
    }
    
    public static float map01( float value, float min, float max )
    {
        return ( value - min ) * 1f / ( max - min );
    }

}
