importScripts(
  "https://www.gstatic.com/firebasejs/9.9.3/firebase-app-compat.js"
);
importScripts(
  "https://www.gstatic.com/firebasejs/9.9.3/firebase-messaging-compat.js"
);

const firebaseConfig = {
  apiKey: "AIzaSyB-4-LZa-jrL-U-PLPIx8zw2y9VbotYA-M",
  authDomain: "task-management-system-d2811.firebaseapp.com",
  projectId: "task-management-system-d2811",
  storageBucket: "task-management-system-d2811.appspot.com",
  messagingSenderId: "599123102770",
  appId: "1:599123102770:web:182d288d4590e19918402b",
};

firebase.initializeApp(firebaseConfig);

const messaging = firebase.messaging();

messaging.onBackgroundMessage(function (payload) {
  const title = payload.notification.title;
  const options = {
    body: payload.notification.body,
  };

  /**
   * Use notification messages when you want FCM to handle displaying a notification on your
   * client app's behalf. Use data messages when you want to process the messages on your
   * client app.
   */
  //self.registration.showNotification(title, options);
});
