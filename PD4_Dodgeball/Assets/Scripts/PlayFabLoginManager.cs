using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;

public class PlayFabLoginManager : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private TMP_InputField _emailInput;
    [SerializeField]
    private TMP_InputField _passwordInput;
    [SerializeField]
    private TMP_InputField _displayNameInput;

    [Header("UI")]
    [SerializeField]
    private TMP_Text _errorText;
    [SerializeField]
    private TMP_Text _playerNameText;

    [SerializeField, Space(15)]
    private UnityEvent _onLoginSucces = new UnityEvent();

    private void Start()
    {
        _errorText.text = "";
        _playerNameText.text = "";
        PlayFabSettings.staticSettings.TitleId = "1E8EDF";
        Debug.Log(PlayFabSettings.staticSettings.TitleId);
    }

    public void Register()
    {
        _errorText.text = "";

        if (string.IsNullOrWhiteSpace(_emailInput.text))
        {
            _errorText.text = "Enter an email.";
            return;
        }

        if (string.IsNullOrWhiteSpace(_passwordInput.text))
        {
            _errorText.text = "Enter a password.";
            return;
        }

        if (string.IsNullOrWhiteSpace(_displayNameInput.text))
        {
            _errorText.text = "Enter a display name.";
            return;
        }

        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
        {
            Email = _emailInput.text,
            Password = _passwordInput.text,
            Username = _displayNameInput.text,
            RequireBothUsernameAndEmail = true
        };

        PlayFabClientAPI.RegisterPlayFabUser(
            request,
            OnRegisterSuccess,
            OnPlayFabError
        );
    }

    public void Login()
    {
        _errorText.text = "";

        LoginWithEmailAddressRequest request =
            new LoginWithEmailAddressRequest
            {
                Email = _emailInput.text,
                Password = _passwordInput.text
            };

        PlayFabClientAPI.LoginWithEmailAddress(
            request,
            OnLoginSuccess,
            OnPlayFabError
        );
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        UpdateDisplayName(_displayNameInput.text);

        _errorText.text = "Registration successful!";
    }

    private void OnLoginSuccess(LoginResult result)
    {
        GetPlayerDisplayName();

        _errorText.text = "Login successful!";

        _onLoginSucces.Invoke();
    }

    private void UpdateDisplayName(string displayName)
    {
        UpdateUserTitleDisplayNameRequest request =
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = displayName
            };

        PlayFabClientAPI.UpdateUserTitleDisplayName(
            request,
            result =>
            {
                _playerNameText.text = result.DisplayName;
            },
            OnPlayFabError
        );
    }

    private void GetPlayerDisplayName()
    {
        PlayFabClientAPI.GetAccountInfo(
            new GetAccountInfoRequest(),
            result =>
            {
                string displayName =
                    result.AccountInfo.TitleInfo.DisplayName;

                _playerNameText.text = displayName;
            },
            OnPlayFabError
        );
    }

    private void OnPlayFabError(PlayFabError error)
    {
        switch (error.Error)
        {
            case PlayFabErrorCode.InvalidEmailAddress:
                _errorText.text = "Invalid email address.";
                break;

            case PlayFabErrorCode.InvalidPassword:
                _errorText.text = "Invalid password.";
                break;

            case PlayFabErrorCode.EmailAddressNotAvailable:
                _errorText.text = "Email already registered.";
                break;

            case PlayFabErrorCode.UsernameNotAvailable:
                _errorText.text = "Display name already taken.";
                break;

            case PlayFabErrorCode.AccountNotFound:
                _errorText.text = "Account not found.";
                break;

            default:
                _errorText.text = error.ErrorMessage;
                break;
        }

        Debug.LogError(error.GenerateErrorReport());
    }
}