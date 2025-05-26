# test_calcul_indicateurs.py
import unittest
from calcul_indicateurs import (
    volume_moyenne, volume_relatif, volume_relatif_moyenne, detect_divergence_rsi, context_spy_from_value
)

class TestCalculIndicateurs(unittest.TestCase):
    def test_volume_moyenne_valide(self):
        vols = [100] * 20
        self.assertAlmostEqual(volume_moyenne(vols), 100.0)
    def test_volume_moyenne_invalide(self):
        vols = [100, 110, 120]
        self.assertIsNone(volume_moyenne(vols))

    def test_volume_relatif_valide(self):
        self.assertAlmostEqual(volume_relatif(200, 100), 2.0)
    def test_volume_relatif_invalide_zero(self):
        self.assertIsNone(volume_relatif(200, 0))
    def test_volume_relatif_invalide_none(self):
        self.assertIsNone(volume_relatif(200, None))

    def test_volume_relatif_moyenne_valide(self):
        rels = [1, 1.2, 0.8, 1.1, 1.3, 0.9]
        self.assertAlmostEqual(volume_relatif_moyenne(rels), 1.05)
    def test_volume_relatif_moyenne_invalide(self):
        rels = [1, 1.2]
        self.assertIsNone(volume_relatif_moyenne(rels))

    def test_detect_divergence_rsi_valide(self):
        prices = [105, 102, 100]
        rsis = [30, 32, 35]
        self.assertTrue(detect_divergence_rsi(prices, rsis))
    def test_detect_divergence_rsi_invalide(self):
        prices = [100, 102, 105]
        rsis = [35, 32, 30]
        self.assertFalse(detect_divergence_rsi(prices, rsis))

    def test_context_spy_bullish(self):
        self.assertEqual(context_spy_from_value(2.0), "bullish")
    def test_context_spy_bearish(self):
        self.assertEqual(context_spy_from_value(-2.0), "bearish")
    def test_context_spy_neutral(self):
        self.assertEqual(context_spy_from_value(0.5), "neutral")

if __name__ == "__main__":
    unittest.main()
