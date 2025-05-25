import unittest
import sys
import os
sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../../')))

from strategies import (
    strategie_1_pullback_rsi_ema,
    strategie_2_breakout_volume,
    strategie_3_divergence_rsi_pattern,
    strategie_4_short_surchat,
    strategie_5_compression_breakout,
)

class TestStrategies(unittest.TestCase):

    def test_strategie_1_valid(self):
        signal = {
            "rsi14": 35,
            "close": 105,
            "ema50": 100,
            "ema20": 110,
            "volume_relatif": 1.5,
            "context_spy": "bullish"
        }
        self.assertTrue(strategie_1_pullback_rsi_ema(signal))

    def test_strategie_1_invalid(self):
        signal = {
            "rsi14": 50,
            "close": 95,
            "ema50": 100,
            "ema20": 98,
            "volume_relatif": 1.0,
            "context_spy": "bearish"
        }
        self.assertFalse(strategie_1_pullback_rsi_ema(signal))

    def test_strategie_2_valid(self):
        signal = {
            "close": 103,
            "bb_upper": 102,
            "rsi5": 70,
            "volume_relatif": 2.5,
            "macd_histogram": 0.5
        }
        self.assertTrue(strategie_2_breakout_volume(signal))

    def test_strategie_2_invalid(self):
        signal = {
            "close": 102,
            "bb_upper": 102,
            "rsi5": 60,
            "volume_relatif": 1.0,
            "macd_histogram": -0.2
        }
        self.assertFalse(strategie_2_breakout_volume(signal))

    def test_strategie_3_valid(self):
        signal = {
            "divergence_rsi": True,
            "pattern": "bullish_engulfing",
            "rsi14": 35,
            "close": 98,
            "bb_mid": 100,
            "volume_relatif": 1.3,
            "context_spy": "neutral"
        }
        self.assertTrue(strategie_3_divergence_rsi_pattern(signal))

    def test_strategie_3_invalid(self):
        signal = {
            "divergence_rsi": False,
            "pattern": "none",
            "rsi14": 50,
            "close": 101,
            "bb_mid": 100,
            "volume_relatif": 1.0,
            "context_spy": "bearish"
        }
        self.assertFalse(strategie_3_divergence_rsi_pattern(signal))

    def test_strategie_4_valid(self):
        signal = {
            "rsi14": 80,
            "close": 95,
            "ema20": 100,
            "pattern": "bearish_engulfing",
            "volume_relatif": 2.0,
            "context_spy": "bearish"
        }
        self.assertTrue(strategie_4_short_surchat(signal))

    def test_strategie_4_invalid(self):
        signal = {
            "rsi14": 60,
            "close": 105,
            "ema20": 100,
            "pattern": "none",
            "volume_relatif": 1.0,
            "context_spy": "neutral"
        }
        self.assertFalse(strategie_4_short_surchat(signal))

    def test_strategie_5_valid(self):
        signal = {
            "bb_width": 0.015,
            "volume_relatif_moy6": 0.6,
            "ema20": 100,
            "ema50": 100.5,
            "pattern": "doji"
        }
        self.assertTrue(strategie_5_compression_breakout(signal))

    def test_strategie_5_invalid(self):
        signal = {
            "bb_width": 0.03,
            "volume_relatif_moy6": 1.2,
            "ema20": 100,
            "ema50": 102,
            "pattern": "none"
        }
        self.assertFalse(strategie_5_compression_breakout(signal))

if __name__ == '__main__':
    print("Lancement des tests unitaires...")
    unittest.main(verbosity=2)
    #unittest.main()
