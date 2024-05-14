using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    [SerializeField] private List<Book> _books;
    [SerializeField] private Button _nextSceneButton;

    private Book _activeBook;

    void Start()
    {
        foreach (var book in _books)
        {
            book.gameObject.SetActive(false);
        }

        if (SceneChanger.prevScene == SceneChanger.Scenes.Respiracion) //Si viene de respiración, activamos el libro 2
        {
            _books[1].gameObject.SetActive(true);
            _activeBook = _books[1];
        }
        else if (SceneChanger.prevScene == SceneChanger.Scenes.HarmonyHeaven || SceneChanger.prevScene == SceneChanger.Scenes.RitmoVejetal)
        {
            _books[2].gameObject.SetActive(true);
            _activeBook = _books[2];
        }
        else if (SceneChanger.prevScene == SceneChanger.Scenes.PaisajeSonoro)
        {
            _books[3].gameObject.SetActive(true);
            _activeBook = _books[3];
        }
        else if (SceneChanger.prevScene == SceneChanger.Scenes.MelodiaFloral)
        {
            _books[4].gameObject.SetActive(true);
            _activeBook = _books[4];
        }
        else
        {
            _books[0].gameObject.SetActive(true);
            _activeBook = _books[0];
        }

        _activeBook.OnFlip.AddListener(CheckPage);
        //NextSceneButton.onClick.AddListener();
    }

    private void CheckPage()
    {
        if(_activeBook.currentPage >= _activeBook.bookPages.Length)
        {
            _nextSceneButton.gameObject.SetActive(true);
        }
    }


}
