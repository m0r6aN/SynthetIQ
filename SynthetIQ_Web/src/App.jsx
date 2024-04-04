import React, { useState, useEffect } from "react";
import "./App.css";
import { v4 as uuidv4 } from "uuid";

function App() {
  const [messages, setMessages] = useState([]);
  const [input, setInput] = useState("");
  const [conversationId, setConversationId] = useState(null);

  useEffect(() => {
    // Generate a new ConversationId when the component mounts
    setConversationId(uuidv4());
  }, []);

  const startNewConversation = () => {
    // Generate a new ConversationId when the user starts a new conversation
    setConversationId(uuidv4());
    setMessages([]);
  };

  const sendMessage = async () => {
    if (!input.trim()) return;

    const latestMessage = { id: Date.now(), text: input, sender: "user" };
    setMessages([...messages, latestMessage]);
    setInput("");

    const payload = {
      ConversationId: conversationId,
      NewMessage: input,
    };

    const controller = new AbortController();
    const signal = controller.signal;

    try {
      // Call the Azure Function
      const response = await fetch("http://localhost:7234/api/OpenAiFunction", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
        signal: signal,
      });

      if (response.ok) {
        const replyText = await response.json();
        const newMessage = {
          id: Date.now() + Math.random(), // Ensure unique ID for each message
          text: replyText, // This is now the AI response
          sender: "system",
        };
        setMessages((messages) => [...messages, newMessage]);
      }
    } catch (error) {
      if (error.name === "AbortError") {
        console.log("Fetch aborted");
      } else {
        console.error("Failed to send message:", error);
        // Optionally, inform the user that sending the message failed
      }
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <h2>Chat with AI</h2>
      </header>
      <div className="chat-box">
        {messages.map((message) => (
          <div key={message.id} className={`message ${message.sender}`}>
            {message.text}
          </div>
        ))}
      </div>
      <input
        value={input}
        onChange={(e) => setInput(e.target.value)}
        onKeyDown={(e) => e.key === "Enter" && sendMessage()}
        placeholder="Say something to OpenAI..."
      />
      <button onClick={sendMessage}>Send</button>
      <button onClick={startNewConversation}>Start new conversation</button>
    </div>
  );
}
export default App;
