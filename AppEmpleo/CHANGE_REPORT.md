# JobTI – UI/Code Quality Refactor Report (2026-01-10)

This report summarizes the UI refactor and code-quality improvements applied to the Razor Pages project.

## ✅ Goals covered

1. **UI similar to the provided “plantilla”**: Implemented a template-like *job board* UI (search bar + results grid + detail panel) on `Pages/Application/Home.cshtml`.
2. **Added JavaScript for more dynamism**: Implemented job card selection, live filtering, and improved layout behavior.
3. **CSS class renaming to English + BEM**: Updated layouts and pages to use English BEM-style naming: `block__element--modifier`.
4. **One CSS file per Razor Page**: Added/linked a dedicated CSS file for each Razor Page.
5. **Correct CSS loading via layouts**: Layouts now load shared CSS + allow each page to inject its own CSS via a `Styles` section.

## ✨ UI changes

### Application/Home (candidate)
- New *job board* layout inspired by the provided image:
  - Top search bar (query + location + button)
  - Results list (cards)
  - Detail panel (sticky on desktop)
  - Apply form inside the detail panel

### Session layout
- Sidebar layout is now based on a responsive “shell” layout with a toggle button.
- Sidebar open state is persisted in `localStorage`.

### Guest layout
- Simplified header/navigation and standardized buttons.

## 🧠 JavaScript changes

- `wwwroot/js/application-home.js`
  - Click a card to populate the detail panel.
  - Live search/filter by query and location.
  - Keeps the first visible card selected when filters change.

- `wwwroot/js/layout.js`
  - Sidebar toggle behavior.
  - Persist open/closed state.
  - Close on outside click (mobile) and on ESC.

- `wwwroot/js/styles.js`
  - Global focus helpers for form controls (`.form-field__input--active`).
  - Removed old offer overlay logic to avoid page-wide side effects.

## 🎨 CSS architecture

### Shared
- `wwwroot/css/base.css`
  - Tokens (colors, radius, shadows)
  - Shared components: `.btn`, `.chip`, `.card`, `.form-field*`

### Layouts
- `wwwroot/css/layouts/guest.css`
- `wwwroot/css/layouts/session.css`

### Per-page CSS (one per Razor Page)
- `wwwroot/css/pages/index.css`
- `wwwroot/css/pages/login.css`
- `wwwroot/css/pages/register-create-account.css`
- `wwwroot/css/pages/register-success.css`
- `wwwroot/css/pages/privacy.css`
- `wwwroot/css/pages/error.css`
- `wwwroot/css/pages/application-home.css`
- `wwwroot/css/pages/application-evaluations.css`

## 🧾 Razor Pages updated

- `Pages/Shared/_Layout.cshtml`
- `Pages/Shared/_LayoutSession.cshtml`
- `Pages/Index.cshtml`
- `Pages/Login/Login.cshtml`
- `Pages/Register/CreateAccount.cshtml`
- `Pages/Register/RegisterSuccess.cshtml`
- `Pages/Privacy.cshtml`
- `Pages/Error.cshtml`
- `Pages/Application/Home.cshtml`
- `Pages/Application/Evaluations.cshtml`

## 🔧 Build verification

- `dotnet build` succeeded after the refactor.

## 🧪 How to test manually

1. Run the project and open:
   - `/` (Index)
   - `/Login/Login`
   - `/Register/CreateAccount`
   - `/Application/Home`
   - `/Application/Evaluations`
2. On `/Application/Home` (candidate):
   - Click different job cards and verify the detail panel updates.
   - Use the search input and location dropdown to filter results.
   - Upload a PDF and click **Apply Now**.
3. Toggle the sidebar and refresh the page to confirm it remembers the state.

## Notes / Follow-ups (optional)

- Some legacy CSS files remain in `wwwroot/css/` (e.g., `offers.css`, `forms.css`). They are no longer loaded by layouts after this refactor.
- If you want, we can extend the template-like style to recruiter flows and add a dedicated recruiter UI section inside `application-home.css`.
