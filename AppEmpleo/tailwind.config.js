// tailwind.config.js
module.exports = {
  content: [
    "./**/*.cshtml",
    "./**/*.html",
    "./**/*.js"
  ],
  theme: {
    extend: {
      fontFamily: {
        primary: ["var(--font-primary)"],
        secondary: ["var(--font-secondary)"]
      }
    }
  },
  plugins: []
}
