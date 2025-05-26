import unittest
import os
from context_spy import get_context_spy

class TestContextSpy(unittest.TestCase):
    def test_get_context_spy(self):
        # Teste pour un intervalle classique (15m)
        interval = "15m"
        try:
            context = get_context_spy(interval)
            self.assertIn(context, ["bullish", "neutral", "bearish"])
            print(f"context_spy pour {interval} = {context}")
        except Exception as e:
            self.fail(f"get_context_spy a levé une exception: {e}")

if __name__ == "__main__":
    unittest.main()
