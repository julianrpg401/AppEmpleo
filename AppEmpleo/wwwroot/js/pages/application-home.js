document.addEventListener('DOMContentLoaded', () => {
  const cards = Array.from(document.querySelectorAll('.job-card'));
  const resultsCount = document.getElementById('jobResultsCount');

  const detailTitle = document.querySelector('.job-detail__title');
  const detailSubtitle = document.querySelector('.job-detail__subtitle');
  const detailDescription = document.getElementById('jobDetailDescription');
  const detailTags = document.getElementById('jobDetailTags');
  const detailSalary = document.getElementById('jobDetailSalary');
  const detailDates = document.getElementById('jobDetailDates');
  const detailLocation = document.getElementById('jobDetailLocation');

  const applyOfferId = document.getElementById('jobApplyOfferId');
  const applyFile = document.getElementById('jobApplyFile');

  const queryInput = document.getElementById('jobSearchQuery');
  const locationSelect = document.getElementById('jobSearchLocation');
  const searchButton = document.getElementById('jobSearchButton');

  if (cards.length === 0) return;

  const clearActive = () => cards.forEach((c) => c.classList.remove('job-card--active'));

  const renderCount = (count) => {
    if (!resultsCount) return;
    resultsCount.textContent = `${count} results found`;
  };

  const setDetail = (card) => {
    clearActive();
    card.classList.add('job-card--active');

    const title = card.dataset.title || 'Job offer';
    const recruiter = card.dataset.recruiter || '';
    const description = card.dataset.description || '';
    const salary = card.dataset.salary || '--';
    const dates = card.dataset.dates || '--';
    const location = card.dataset.location || '--';
    const offerId = card.dataset.offerId;

    if (detailTitle) detailTitle.textContent = title;
    if (detailSubtitle) detailSubtitle.textContent = recruiter;
    if (detailDescription) detailDescription.textContent = description;
    if (detailSalary) detailSalary.textContent = salary;
    if (detailDates) detailDates.textContent = dates;
    if (detailLocation) detailLocation.textContent = location;

    if (detailTags) {
      detailTags.innerHTML = '';
      card.querySelectorAll('[data-chip]')?.forEach((chip) => {
        const span = document.createElement('span');
        span.className = 'chip';
        span.textContent = chip.textContent;
        detailTags.appendChild(span);
      });
    }

    if (applyOfferId && offerId) {
      applyOfferId.value = offerId;
    }
  };

  const applyFilters = () => {
    const q = (queryInput?.value || '').trim().toLowerCase();
    const loc = (locationSelect?.value || '').trim().toLowerCase();

    let visible = 0;
    cards.forEach((c) => {
      const title = (c.dataset.title || '').toLowerCase();
      const location = (c.dataset.location || '').toLowerCase();
      const matchesQuery = q.length === 0 || title.includes(q);
      const matchesLoc = loc.length === 0 || location.includes(loc);

      const show = matchesQuery && matchesLoc;
      c.style.display = show ? '' : 'none';
      if (show) visible += 1;
    });

    renderCount(visible);

    // If active card is hidden, select the first visible.
    const active = cards.find((c) => c.classList.contains('job-card--active'));
    if (!active || active.style.display === 'none') {
      const firstVisible = cards.find((c) => c.style.display !== 'none');
      if (firstVisible) setDetail(firstVisible);
    }
  };

  cards.forEach((card) => {
    card.addEventListener('click', (e) => {
      const tag = e.target.tagName.toLowerCase();
      if (tag === 'input' || tag === 'button' || e.target.closest('form')) return;
      setDetail(card);
    });
  });

  // Search UX
  queryInput?.addEventListener('input', applyFilters);
  locationSelect?.addEventListener('change', applyFilters);
  searchButton?.addEventListener('click', (e) => {
    e.preventDefault();
    applyFilters();
    applyFile?.focus();
  });

  // Initial
  renderCount(cards.length);
  setDetail(cards[0]);
});
