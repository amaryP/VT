import sys, os
sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../src')))
from src.recup_signals import fetch_patterns
import pytest

def test_fetch_patterns_env_key(monkeypatch):
    # Mock la clé API dans l'env
    monkeypatch.setenv("KEY_TAAPI_IO", "fake_key")
    # Mock requests.post pour ne pas faire de vrai call
    import requests
    def fake_post(*args, **kwargs):
        class FakeResp:
            def json(self):
                return {"data": [
                    {"indicator": "cupAndHandle", "result": {"match": True}},
                    {"indicator": "ascendingTriangle", "result": {"match": False}},
                ]}
            @property
            def status_code(self):
                return 200
        return FakeResp()
    monkeypatch.setattr(requests, "post", fake_post)
    res = fetch_patterns("AAPL", "1d", ["cupAndHandle", "ascendingTriangle"])
    assert res == "cupAndHandle"

def test_fetch_patterns_no_match(monkeypatch):
    monkeypatch.setenv("KEY_TAAPI_IO", "fake_key")
    import requests
    def fake_post(*args, **kwargs):
        class FakeResp:
            def json(self):
                return {"data": [
                    {"indicator": "cupAndHandle", "result": {"match": False}},
                    {"indicator": "ascendingTriangle", "result": {"match": False}},
                ]}
            @property
            def status_code(self):
                return 200
        return FakeResp()
    monkeypatch.setattr(requests, "post", fake_post)
    res = fetch_patterns("AAPL", "1d", ["cupAndHandle", "ascendingTriangle"])
    assert res is None
