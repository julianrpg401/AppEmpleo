(() => {
    const shell = document.querySelector('[data-shell]');
    const sidebar = document.querySelector('.sidebar');
    const toggle =
        document.querySelector('[data-sidebar-toggle]') ||
        document.getElementById('sidebarToggle') ||
        document.getElementById('arrow');

    if (!shell || !sidebar || !toggle) return;

    if (!sidebar.id) {
        sidebar.id = 'sidebar';
    }

    const ensureInteractiveToggle = () => {
        const interactiveTags = ['BUTTON', 'INPUT'];
        if (!interactiveTags.includes(toggle.tagName)) {
            toggle.setAttribute('role', 'button');
            toggle.setAttribute('tabindex', '0');
        }

        toggle.setAttribute('aria-controls', sidebar.id);
    };

    const STORAGE_KEY = 'jobti.sidebar.open';

    const OPEN_CLASS = 'session-layout--sidebar-open';

    const setOpen = (isOpen) => {
        shell.classList.toggle(OPEN_CLASS, isOpen);
        toggle.setAttribute('aria-expanded', String(isOpen));
        toggle.setAttribute('aria-pressed', String(isOpen));
        try {
            localStorage.setItem(STORAGE_KEY, String(isOpen));
        } catch {
            // ignore
        }
    };

    const getSaved = () => {
        try {
            return localStorage.getItem(STORAGE_KEY) === 'true';
        } catch {
            return false;
        }
    };

    ensureInteractiveToggle();

    // Default: collapsed on desktop, closed on mobile.
    setOpen(getSaved());

    toggle.addEventListener('click', () => {
        setOpen(!shell.classList.contains(OPEN_CLASS));
    });

    toggle.addEventListener('keydown', (event) => {
        if (event.key !== 'Enter' && event.key !== ' ') return;
        event.preventDefault();
        toggle.click();
    });

    // Close on outside click (mobile)
    document.addEventListener('click', (e) => {
        const isMobile = window.matchMedia('(max-width: 900px)').matches;
        if (!isMobile) return;
        if (!shell.classList.contains(OPEN_CLASS)) return;

        const clickedInsideSidebar = sidebar.contains(e.target);
        const clickedToggle = toggle.contains(e.target);
        if (!clickedInsideSidebar && !clickedToggle) {
            setOpen(false);
        }
    });

    // Close on ESC
    document.addEventListener('keydown', (e) => {
        if (e.key !== 'Escape') return;
        setOpen(false);
    });
})();