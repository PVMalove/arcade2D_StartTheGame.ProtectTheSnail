const library = {
  
  $yandexGames: {
    isInitialized: false,
    isInitializeCalled: false,
    sdk: undefined,

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
        });
      }
      
      yandexGames.isInitialized = true;
      dynCall('v', successCallbackPtr, [])
    },
  },

  YandexGamesSdkInitialize: function (successCallbackPtr) {
    yandexGames.yandexGamesSdkInitialize(successCallbackPtr);
  },

  GetYandexGamesSdkIsInitialized: function () {
    return yandexGames.isInitialized;
  },
}

autoAddDeps(library, '$yandexGames');
mergeInto(LibraryManager.library, library);