from fastapi import FastAPI
from pydantic import BaseModel
import joblib

# Завантажуємо модель
pipeline = joblib.load("pipeline.pkl")

app = FastAPI()

class Comment(BaseModel):
    text: str

@app.post("/predict")
def predict(comment: Comment):
    label = pipeline.predict([comment.text])[0]
    return {"toxic": int(label)}
