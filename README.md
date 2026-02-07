# 📄 Plagiarism Detection System  
*A Hybrid Web-Based Application using .NET and Machine Learning*

## 🔍 Overview
The Plagiarism Detection System is a web-based application designed to detect similarity between text documents and identify potential plagiarism. The system combines a **.NET Core MVC web application** for user interaction with a **Python-based machine learning module** for text preprocessing and similarity analysis.

This project demonstrates the integration of **web technologies, backend services, databases, and basic machine learning techniques** in a single system.

---

## 🎯 Problem Statement
With the increasing availability of digital content, plagiarism has become a major concern in academic and content-driven environments. Manual detection is time-consuming and unreliable, creating the need for an automated system that can efficiently analyze and compare text submissions.

---

## 💡 Proposed Solution
The system allows users to submit text content through a web interface. The submitted text is processed and compared against existing records to determine similarity levels using machine learning–based text analysis techniques.

---

## 🏗️ System Architecture
The project follows a **hybrid architecture**:

- **Frontend & Backend (ASP.NET Core MVC):**
  - Handles user interface
  - Manages submissions and history
  - Stores data in the database

- **Machine Learning Engine (Python):**
  - Performs text preprocessing
  - Computes similarity scores between documents
  - Returns plagiarism results

---

## 🛠️ Technology Stack

### Web Application
- ASP.NET Core MVC
- C#
- HTML, CSS, JavaScript

### Machine Learning
- Python
- Scikit-learn
- NLTK
- NumPy
- Pandas

### Database
- SQLite / MongoDB (for storing submissions and history)

---

## 📂 Project Structure
Plagiarism-Detection-System
│
├── dotnet-app/
│ ├── Controllers/
│ ├── Models/
│ ├── Services/
│ ├── Views/
│ ├── wwwroot/
│ ├── Properties/
│ ├── Program.cs
│ ├── appsettings.json
│ └── PlagiarismChecker.Web.csproj
│
├── ml-engine/
│ ├── plagiarism_detector.py
│ ├── preprocessing.py
│ ├── similarity.py
│ └── requirements.txt
│
├── database/
│ └── plagiarism.db
│
└── README.md

---

## ⚙️ How the System Works
1. User submits text through the web interface.
2. The .NET backend processes the request and stores submission details.
3. Text data is passed to the Python ML module.
4. The ML engine preprocesses the text and computes similarity scores.
5. Plagiarism results are returned and displayed to the user.

---

## 🚀 Features
- Text submission and comparison
- Similarity score generation
- Submission history tracking
- Simple and intuitive user interface
- Modular separation of web and ML components

---

## 🔮 Future Enhancements
- Support for multiple document formats (PDF, DOCX)
- Advanced NLP techniques (TF-IDF, embeddings)
- User authentication and role management
- API-based integration between .NET and Python
- Cloud-based deployment

---

## 🎓 Academic Context
This project was developed as a **learning-oriented machine learning and web development project**, focusing on practical implementation of plagiarism detection concepts and cross-technology integration.

---

## 🤝 Contribution
This repository represents my individual implementation and learning contribution to the project. Suggestions and improvements are welcome.


## 📂 Project Structure
