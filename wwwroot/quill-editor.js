window.journalQuill = (function () {
  const quillInstances = {};

  function ensureQuill(elementId) {
    const el = document.getElementById(elementId);
    if (!el) {
      throw new Error(`Quill mount element not found: ${elementId}`);
    }

    if (!window.Quill) {
      throw new Error('Quill is not loaded');
    }

    // Check if instance already exists for this element
    if (!quillInstances[elementId]) {
      quillInstances[elementId] = new window.Quill(el, {
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

    return quillInstances[elementId];
  }

  return {
    init: function (elementId, initialHtml) {
      const q = ensureQuill(elementId);
      if (typeof initialHtml === 'string' && initialHtml.length > 0) {
        q.root.innerHTML = initialHtml;
      }
      return q;
    },
    getHtml: function (elementId) {
      const q = elementId ? quillInstances[elementId] : Object.values(quillInstances)[0];
      return q ? q.root.innerHTML : '';
    },
    setHtml: function (elementId, html) {
      const q = elementId ? quillInstances[elementId] : Object.values(quillInstances)[0];
      if (q) {
        q.root.innerHTML = html || '';
      }
    },
    clear: function (elementId) {
      const q = elementId ? quillInstances[elementId] : Object.values(quillInstances)[0];
      if (q) {
        q.setText('');
      }
    },
    destroy: function (elementId) {
      if (quillInstances[elementId]) {
        delete quillInstances[elementId];
      }
    }
  };
})();
