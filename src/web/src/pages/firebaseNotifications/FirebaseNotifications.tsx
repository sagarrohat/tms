import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import { initializeApp } from "firebase/app";
import { getMessaging, getToken, onMessage } from "firebase/messaging";
import { setMessagingToken } from "./actionCreators";
import { addToast } from "../../core/features/toast/actionCreators";

export function FirebaseNotifications() {
  const dispatch = useDispatch();

  let firebaseConfig = {
    apiKey: process.env.REACT_APP_FIREBASE_API_KEY,
    authDomain: process.env.REACT_APP_FIREBASE_AUTH_DOMAIN,
    projectId: process.env.REACT_APP_FIREBASE_PROJECT_ID,
    storageBucket: process.env.REACT_APP_FIREBASE_STORAGE_BUCKET,
    messagingSenderId: process.env.REACT_APP_FIREBASE_MESSAGING_SENDER_ID,
    appId: process.env.REACT_APP_FIREBASE_APP_ID,
  };


  let vapidKey = String(process.env.REACT_APP_FIREBASE_VAPID_KEY);
  const app = initializeApp(firebaseConfig);
  const messaging = getMessaging(app);

  const getMessagingToken = () => {
    return getToken(messaging, {
      vapidKey: vapidKey,
    })
      .then((currentToken) => {
        if (currentToken) {
          dispatch(setMessagingToken(currentToken));
        } else {
          // TODO: Show error message to user
        }
      })
      .catch((err) => {
        // TODO: Show error message to user
      });
  };

  onMessage(messaging, (payload) => {
    // Show this message
    const { title, body } = payload.notification;
    dispatch(addToast({ message: title + ": " + body, type: "info" }));
  });

  useEffect(() => {
    getMessagingToken();
  }, []);

  return null;
}
