// Web Audio synthesizer. .NET sends a tune as absolute frequencies and times (see Tune.Render in GeoDistance.Core).

let ctx = null;

/** Creates / resumes the AudioContext. Must run inside a user gesture (a click). */
export function unlock() {
  const AudioContextType = window.AudioContext || window.webkitAudioContext;
  if (!AudioContextType) return;
  ctx ??= new AudioContextType();
  if (ctx.state === "suspended") ctx.resume();
}

/** @param {{wave: string, style: string, notes: {frequency: number, start: number, duration: number}[], droneFrequencies: number[], totalSeconds: number}} sequence */
export function play(sequence) {
  if (!ctx) return;
  const start = ctx.currentTime + 0.05;

  for (const note of sequence.notes) {
    playNote(note.frequency, start + note.start, note.duration, sequence);
  }

  // tanpura-like drone under the melody
  for (const frequency of sequence.droneFrequencies) {
    const end = start + sequence.totalSeconds;
    const osc = ctx.createOscillator();
    const gain = ctx.createGain();
    osc.type = "sine";
    osc.frequency.value = frequency;
    gain.gain.setValueAtTime(0.0001, start);
    gain.gain.exponentialRampToValueAtTime(0.07, start + 0.2);
    gain.gain.setValueAtTime(0.07, end - 0.2);
    gain.gain.exponentialRampToValueAtTime(0.0001, end + 0.4);
    osc.connect(gain).connect(ctx.destination);
    osc.start(start);
    osc.stop(end + 0.5);
  }
}

function playNote(frequency, time, duration, tune) {
  const osc = ctx.createOscillator();
  const filter = ctx.createBiquadFilter();
  const amp = ctx.createGain();
  osc.type = tune.wave;
  osc.frequency.value = frequency;
  filter.type = "lowpass";
  filter.frequency.value = Math.min(frequency * 5, 3500);
  osc.connect(filter).connect(amp).connect(ctx.destination);

  let end;
  if (tune.style === "pluck") {
    // quick attack, long decay (koto / harp / music box)
    end = time + Math.max(duration * 1.8, 0.35);
    amp.gain.setValueAtTime(0.0001, time);
    amp.gain.exponentialRampToValueAtTime(0.3, time + 0.006);
    amp.gain.exponentialRampToValueAtTime(0.0001, end);
  } else {
    // held notes (reed / flute / fiddle)
    end = time + duration + 0.06;
    amp.gain.setValueAtTime(0.0001, time);
    amp.gain.exponentialRampToValueAtTime(0.2, time + 0.03);
    amp.gain.setValueAtTime(0.2, time + duration * 0.85);
    amp.gain.exponentialRampToValueAtTime(0.0001, end);

    if (tune.style === "sustain" || tune.wave === "sawtooth") {
      // gentle vibrato
      const lfo = ctx.createOscillator();
      const depth = ctx.createGain();
      lfo.frequency.value = 5.5;
      depth.gain.value = frequency * 0.006;
      lfo.connect(depth).connect(osc.frequency);
      lfo.start(time);
      lfo.stop(end);
    }
  }

  osc.start(time);
  osc.stop(end + 0.05);
}
