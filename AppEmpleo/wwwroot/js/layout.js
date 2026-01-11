(() => {
    const shell = document.querySelector('[data-shell]');
    const toggle = document.getElementById('sidebarToggle');
    const sidebar = document.querySelector('.app-sidebar');
    if (!shell || !toggle || !sidebar) return;

    const STORAGE_KEY = 'jobti.sidebar.open';

    const setOpen = (isOpen) => {
        shell.classList.toggle('app-shell--sidebar-open', isOpen);
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

    // Default: collapsed on desktop, closed on mobile.
    setOpen(getSaved());

    toggle.addEventListener('click', () => {
        setOpen(!shell.classList.contains('app-shell--sidebar-open'));
    });

    // Close on outside click (mobile)
    document.addEventListener('click', (e) => {
        const isMobile = window.matchMedia('(max-width: 900px)').matches;
        if (!isMobile) return;
        if (!shell.classList.contains('app-shell--sidebar-open')) return;

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