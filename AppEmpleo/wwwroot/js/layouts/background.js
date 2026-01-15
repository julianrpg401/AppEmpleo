(() => {
  const reduceMotion = window.matchMedia && window.matchMedia('(prefers-reduced-motion: reduce)').matches;
  if (reduceMotion) return;

  const canvas = document.querySelector('[data-bg-waves]');
  if (!(canvas instanceof HTMLCanvasElement)) return;

  const ctx = canvas.getContext('2d', { alpha: true });
  if (!ctx) return;

  let w = 0;
  let h = 0;
  let dpr = 1;
  let rafId = 0;

  const palette = {
    a: 'rgba(255, 179, 200, 0.35)',
    b: 'rgba(129, 60, 80, 0.18)',
    c: 'rgba(255, 236, 241, 0.65)'
  };

  const resize = () => {
    const rect = canvas.getBoundingClientRect();
    w = Math.max(1, Math.floor(rect.width));
    h = Math.max(1, Math.floor(rect.height));
    dpr = Math.min(2, window.devicePixelRatio || 1);
    canvas.width = Math.floor(w * dpr);
    canvas.height = Math.floor(h * dpr);
    ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
  };

  const lerp = (a, b, t) => a + (b - a) * t;

  const drawWave = (time, baseline, amp, freq, color, phase) => {
    ctx.beginPath();
    ctx.moveTo(0, h);
    ctx.lineTo(0, baseline);

    for (let x = 0; x <= w; x += 14) {
      const t = (x / w) * Math.PI * 2;
      const y = baseline + Math.sin(t * freq + time + phase) * amp;
      ctx.lineTo(x, y);
    }

    ctx.lineTo(w, baseline);
    ctx.lineTo(w, h);
    ctx.closePath();
    ctx.fillStyle = color;
    ctx.fill();
  };

  const frame = (ms) => {
    const t = ms * 0.001;

    ctx.clearRect(0, 0, w, h);

    // Soft gradient wash
    const g = ctx.createLinearGradient(0, 0, w, h);
    g.addColorStop(0, palette.c);
    g.addColorStop(1, 'rgba(255,255,255,0)');
    ctx.fillStyle = g;
    ctx.fillRect(0, 0, w, h);

    const base1 = lerp(h * 0.78, h * 0.72, (Math.sin(t * 0.35) + 1) / 2);
    const base2 = lerp(h * 0.55, h * 0.50, (Math.sin(t * 0.28 + 1.2) + 1) / 2);
    const base3 = lerp(h * 0.32, h * 0.28, (Math.sin(t * 0.22 + 2.1) + 1) / 2);

    drawWave(t * 0.7, base1, 16, 1.4, palette.a, 0);
    drawWave(t * 0.55, base2, 12, 1.8, palette.b, 1.6);
    drawWave(t * 0.45, base3, 10, 2.2, 'rgba(255, 198, 213, 0.22)', 2.8);

    rafId = window.requestAnimationFrame(frame);
  };

  const onVisibility = () => {
    if (document.hidden) {
      window.cancelAnimationFrame(rafId);
      rafId = 0;
      return;
    }
    if (!rafId) rafId = window.requestAnimationFrame(frame);
  };

  resize();
  rafId = window.requestAnimationFrame(frame);

  window.addEventListener('resize', resize, { passive: true });
  document.addEventListener('visibilitychange', onVisibility);
})();
