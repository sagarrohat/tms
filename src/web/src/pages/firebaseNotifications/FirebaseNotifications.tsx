import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import { initializeApp } from "firebase/app";
import { getMessaging, getToken, onMessage } from "firebase/messaging";
import { setMessagingToken } from "./actionCreators";

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

  const app = initializeApp(firebaseConfig);
  const messaging = getMessaging(app);

  const getMessagingToken = () => {
    return getToken(messaging, {
      vapidKey:
        "BFRdqBpc--50cWcz1-IQLFlccvh8--ul0g2UGBb2U4cxIBf5eJWqXWxLZQgvlAb1Q8vaAfPL-BkBYTHa4Djl-30",
    })
      .then((currentToken) => {
        if (currentToken) {
          console.log(currentToken);
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
    console.log(payload);
    // Show this message
  });

  useEffect(() => {
    getMessagingToken();
  }, []);

  return null;
}
