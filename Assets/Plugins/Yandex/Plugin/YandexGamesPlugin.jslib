const library = {
  
  $yandexGames: {
    isInitialized: false,
    isInitializeCalled: false,
    isAuthorized: false,
    
    sdk: undefined,
    playerAccount: undefined,

    yandexGamesSdkInitialize: function (successCallbackPtr) {
      if (yandexGames.isInitializeCalled) {
        return;
      }
      yandexGames.isInitializeCalled = true;

      const sdkScript = document.createElement('script');
      sdkScript.src = 'https://yandex.ru/games/sdk/v2';
      document.head.appendChild(sdkScript);
      
      sdkScript.onload = function () {
        window['YaGames'].init().then(function (sdk) {
          yandexGames.sdk = sdk;
          
          const playerAccountInitialization = sdk.getPlayer({ scopes: false }).then(function (playerAccount) {
            if (playerAccount.getMode() !== 'lite') {
              yandexGames.isAuthorized = true;
            }

            yandexGames.playerAccount = playerAccount;
          }).catch(function () { throw new Error('PlayerAccount failed to initialize.'); });

          Promise.allSettled([playerAccountInitialization]).then(function () {
            yandexGames.isInitialized = true;
            dynCall('v', successCallbackPtr, []);
          });
        });        
      }
    },

    playerAccountAuthorize: function (successCallbackPtr, errorCallbackPtr) {
      if (yandexGames.isAuthorized) {
        console.error('Already authorized.');
        dynCall('v', successCallbackPtr, []);
        return;
      }

      yandexGames.sdk.auth.openAuthDialog().then(function () {
        yandexGames.sdk.getPlayer({ scopes: false }).then(function (playerAccount) {
          yandexGames.isAuthorized = true;
          yandexGames.playerAccount = playerAccount;
          dynCall('v', successCallbackPtr, []);
        }).catch(function (error) {
          console.error('authorize failed to update playerAccount. Assuming authorization failed. Error was: ' + error.message);
          yandexGames.invokeErrorCallback(error, errorCallbackPtr);
        });
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    invokeErrorCallback: function (error, errorCallbackPtr) {
      var errorMessage;
      if (error instanceof Error) {
        errorMessage = error.message;
        if (errorMessage === null) { errorMessage = 'SDK API thrown an error with null message.' }
        if (errorMessage === undefined) { errorMessage = 'SDK API thrown an error with undefined message.' }
      } else if (typeof error === 'string') {
        errorMessage = error;
      } else if (error) {
        errorMessage = 'SDK API thrown an unexpected type as error: ' + JSON.stringify(error);
      } else if (error === null) {
        errorMessage = 'SDK API thrown a null as error.';
      } else {
        errorMessage = 'SDK API thrown an undefined as error.';
      }

      const errorUnmanagedStringPtr = yandexGames.allocateUnmanagedString(errorMessage);
      dynCall('vi', errorCallbackPtr, [errorUnmanagedStringPtr]);
      _free(errorUnmanagedStringPtr);
    },

    invokeErrorCallbackIfNotAuthorized: function (errorCallbackPtr) {
      if (!yandexGames.isAuthorized) {
        yandexGames.invokeErrorCallback(new Error('Needs authorization.'), errorCallbackPtr);
        return true;
      }
      return false;
    },

    throwIfSdkNotInitialized: function () {
      if (!yandexGames.isInitialized) {
        throw new Error('SDK is not initialized. Invoke YandexGamesSdk.Initialize() coroutine and wait for it to finish.');
      }
    },
  },
  
  

  YandexGamesSdkInitialize: function (successCallbackPtr) {
    yandexGames.yandexGamesSdkInitialize(successCallbackPtr);
  },

  GetYandexGamesSdkIsInitialized: function () {
    return yandexGames.isInitialized;
  },
  
  PlayerAccountAuthorize: function (successCallbackPtr, errorCallbackPtr) {
    yandexGames.throwIfSdkNotInitialized();

    yandexGames.playerAccountAuthorize(successCallbackPtr, errorCallbackPtr);
  },

  GetPlayerAccountIsAuthorized: function () {
    yandexGames.throwIfSdkNotInitialized();

    return yandexGames.isAuthorized;
  },

  GetPlayerAccountHasPersonalProfileDataPermission: function () {
    yandexGames.throwIfSdkNotInitialized();

    return yandexGames.getPlayerAccountHasPersonalProfileDataPermission();
  },

  PlayerAccountRequestPersonalProfileDataPermission: function (successCallbackPtr, errorCallbackPtr) {
    yandexGames.throwIfSdkNotInitialized();

    yandexGames.playerAccountRequestPersonalProfileDataPermission(successCallbackPtr, errorCallbackPtr);
  },

  PlayerAccountGetProfileData: function (successCallbackPtr, errorCallbackPtr) {
    yandexGames.throwIfSdkNotInitialized();

    yandexGames.playerAccountGetProfileData(successCallbackPtr, errorCallbackPtr);
  },

  PlayerAccountGetPlayerData: function (successCallbackPtr, errorCallbackPtr) {
    yandexGames.throwIfSdkNotInitialized();

    yandexGames.playerAccountGetPlayerData(successCallbackPtr, errorCallbackPtr);
  },

  PlayerAccountSetPlayerData: function (playerDataJsonPtr, successCallbackPtr, errorCallbackPtr) {
    yandexGames.throwIfSdkNotInitialized();

    const playerDataJson = UTF8ToString(playerDataJsonPtr);
    yandexGames.playerAccountSetPlayerData(playerDataJson, successCallbackPtr, errorCallbackPtr);
  },
}

autoAddDeps(library, '$yandexGames');
mergeInto(LibraryManager.library, library);