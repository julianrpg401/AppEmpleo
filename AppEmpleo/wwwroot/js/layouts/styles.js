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
    const nav = header?.querySelector('.guest-header__nav');
    if (!header || !toggle || !nav) return;

    const applyHeight = (value) => {
      nav.style.maxHeight = value;
    };

    const open = () => {
      header.classList.add('guest-header--nav-open');
      applyHeight(`${nav.scrollHeight}px`);
    };

    const close = () => {
      if (!header.classList.contains('guest-header--nav-open')) {
        applyHeight('0px');
        return;
      }

      const currentHeight = `${nav.scrollHeight}px`;
      applyHeight(currentHeight);
      // Force reflow so the browser registers the starting height before collapsing
      void nav.offsetHeight;

      header.classList.remove('guest-header--nav-open');
      requestAnimationFrame(() => {
        applyHeight('0px');
      });
    };

    const toggleOpen = () => {
      if (header.classList.contains('guest-header--nav-open')) {
        close();
        return;
      }

      open();
    };

    nav.addEventListener('transitionend', (event) => {
      if (event.propertyName !== 'max-height') return;
      if (header.classList.contains('guest-header--nav-open')) {
        applyHeight('');
      }
    });

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