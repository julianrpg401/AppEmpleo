(() => {
  const attachFocusHelpers = () => {
    const inputs = document.querySelectorAll('input, textarea, select');
    inputs.forEach((el) => {
      el.addEventListener('focus', () => {
        if (el.classList.contains('form-field__input') || el.classList.contains('form-field__select')) {
          el.classList.add('form-field__input--active');
        }
      });

      el.addEventListener('blur', () => {
        el.classList.remove('form-field__input--active');
      });
    });
  };

  const attachGuestNavToggle = () => {
    const header = document.querySelector('.guest-header');
    const toggle = document.getElementById('guestNavToggle');
    if (!header || !toggle) return;

    const close = () => header.classList.remove('guest-header--nav-open');
    const toggleOpen = () => header.classList.toggle('guest-header--nav-open');

    toggle.addEventListener('click', (e) => {
      e.preventDefault();
      toggleOpen();
    });

    document.addEventListener('click', (e) => {
      if (!header.classList.contains('guest-header--nav-open')) return;
      const clickedToggle = toggle.contains(e.target);
      const clickedInsideHeader = header.contains(e.target);
      if (!clickedToggle && !clickedInsideHeader) close();
    });

    document.addEventListener('keydown', (e) => {
      if (e.key === 'Escape') close();
    });

    // Close when switching to desktop layout
    const mql = window.matchMedia('(min-width: 769px)');
    const onChange = () => {
      if (mql.matches) close();
    };
    if (typeof mql.addEventListener === 'function') {
      mql.addEventListener('change', onChange);
    } else {
      // Safari fallback
      mql.addListener(onChange);
    }
  };

  const attachRadioPillsFallback = () => {
    // If :has() is supported, CSS handles everything.
    const hasHas = (() => {
      try {
        return CSS.supports && CSS.supports('selector(:has(*))');
      } catch {
        return false;
      }
    })();
    if (hasHas) return;

    const groups = document.querySelectorAll('.radio-group');
    groups.forEach((group) => {
      const sync = () => {
        const pills = group.querySelectorAll('.radio-pill');
        pills.forEach((pill) => {
          const input = pill.querySelector('input[type="radio"]');
          pill.classList.toggle('radio-pill--checked', !!input && input.checked);
        });
      };

      group.addEventListener('change', sync);
      sync();
    });
  };

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', () => {
      attachFocusHelpers();
      attachGuestNavToggle();
      attachRadioPillsFallback();
    });
  } else {
    attachFocusHelpers();
    attachGuestNavToggle();
    attachRadioPillsFallback();
  }
})();