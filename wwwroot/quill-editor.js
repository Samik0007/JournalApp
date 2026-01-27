window.journalQuill = (function () {
  let quill;

  function ensureQuill(elementId) {
    const el = document.getElementById(elementId);
    if (!el) {
      throw new Error(`Quill mount element not found: ${elementId}`);
    }

    if (!window.Quill) {
      throw new Error('Quill is not loaded');
    }

    if (!quill) {
      quill = new window.Quill(el, {
        theme: 'snow',
        placeholder: 'Start writing your journal entry here...',
        modules: {
          toolbar: [
            ['bold', 'italic', 'underline', 'strike'],
            [{ header: [1, 2, 3, false] }],
            [{ list: 'ordered' }, { list: 'bullet' }],
            ['link', 'blockquote', 'code-block'],
            ['clean']
          ]
        }
      });
    }

    return quill;
  }

  return {
    init: function (elementId, initialHtml) {
      const q = ensureQuill(elementId);
      if (typeof initialHtml === 'string') {
        q.root.innerHTML = initialHtml;
      }
    },
    getHtml: function () {
      return quill ? quill.root.innerHTML : '';
    },
    setHtml: function (html) {
      if (quill) {
        quill.root.innerHTML = html || '';
      }
    }
  };
})();
