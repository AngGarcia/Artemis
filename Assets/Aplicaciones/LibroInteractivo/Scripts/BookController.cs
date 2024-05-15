using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneChanger;

public class BookController : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private List<Book> _books;
    [SerializeField] private Button _nextSceneButton;

    private Book _activeBook;
    private Scenes _nextScene = Scenes.MainMenu;

    void Start()
    {
        foreach (var book in _books)
        {
            book.gameObject.SetActive(false);
        }
        _nextSceneButton.gameObject.SetActive(false);

        if (SceneChanger.Instance.prevScene == Scenes.Respiracion) //Si viene de respiración, activamos el libro 2
        {
            _books[1].gameObject.SetActive(true);
            _activeBook = _books[1];
            _nextScene = Scenes.HarmonyHeaven;
        }
        else if (SceneChanger.Instance.prevScene == Scenes.HarmonyHeaven || SceneChanger.Instance.prevScene == Scenes.RitmoVegetal)
        {
            _books[2].gameObject.SetActive(true);
            _activeBook = _books[2];
            _nextScene = Scenes.PaisajeSonoro;
        }
        else if (SceneChanger.Instance.prevScene == Scenes.PaisajeSonoro)
        {
            _books[3].gameObject.SetActive(true);
            _activeBook = _books[3];
            _nextScene = Scenes.MelodiaFloral;
        }
        else if (SceneChanger.Instance.prevScene == Scenes.MelodiaFloral)
        {
            _books[4].gameObject.SetActive(true);
            _activeBook = _books[4];
        }
        else
        {
            _books[0].gameObject.SetActive(true);
            _activeBook = _books[0];
            _nextScene = Scenes.Respiracion;
        }

        _activeBook.OnFlip.AddListener(CheckPage);
        _nextSceneButton.onClick.AddListener(NextScene);
    }

    private void OnDestroy()
    {
        _activeBook.OnFlip.RemoveListener(CheckPage);
        _nextSceneButton.onClick.RemoveListener(NextScene);
    }

    private void CheckPage()
    {
        if(_activeBook.currentPage >= _activeBook.bookPages.Length)
        {
            _nextSceneButton.gameObject.SetActive(true);
        }
    }

    private void NextScene()
    {
        SceneChanger.Instance.GoToScene(_nextScene);
    }
}
