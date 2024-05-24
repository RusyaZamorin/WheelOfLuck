using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SampleProjectSpinManager : SpinWheelManager
{
    public Text txtCoinCount, txtHeartCount;

    public override void Start()
    {
        base.Start();
        txtCoinCount.text = GetCoins() + "";
        txtHeartCount.text = GetHearts() + "";
    }

    public override void OnFinishedSpin()
    {
        base.OnFinishedSpin();

        // Here give player the prize.
        switch (selectedItem)
        {
            case 0:
                AddCoin(1000);
                break;
            case 1:
                AddHeart(1);
                break;
            case 2:
                AddCoin(500);
                break;
            case 3:
                AddCoin(100);
                break;
            case 4:
                AddHeart(5);
                break;
            case 5:
                AddCoin(50);
                break;
            case 6:
                AddCoin(2000);
                break;
            case 7:
                AddHeart(3);
                break;
            case 8:
                AddCoin(200);
                break;
        }
    }

    public override void OnSpinButtonClick()
    {
        if (!IsWheelSpinning())
        {
            if (UseCoin(200))
            {
                base.OnSpinButtonClick();
            }
        }
    }

    private int GetCoins()
    {
        return PlayerPrefs.GetInt("coin", 1000);
    }

    private void AddCoin(int value)
    {
        int coins = GetCoins();
        PlayerPrefs.SetInt("coin", coins + value);
        StartCoroutine(AnimateCountText(txtCoinCount, coins, coins + value));
    }

    private bool UseCoin(int value)
    {
        int coins = GetCoins();
        if(coins >= value)
        {
            PlayerPrefs.SetInt("coin", GetCoins() - value);
            StartCoroutine(AnimateCountText(txtCoinCount, coins, coins - value));
            return true;
        }

        return false;
    }

    private int GetHearts()
    {
        return PlayerPrefs.GetInt("heart", 0);
    }

    private void AddHeart(int value)
    {
        int hearts = GetHearts();
        PlayerPrefs.SetInt("heart", hearts + value);
        StartCoroutine(AnimateCountText(txtHeartCount, hearts, hearts + value));
    }

    private IEnumerator AnimateCountText(Text text, int preValue, int nextValue)
    {
        bool increase = true;
        if (nextValue < preValue)
        {
            increase = false;
        }

        float value = nextValue - preValue;

        float t = (Mathf.Abs(value) / 5) * 0.4f;
        if (t > 2.0f) t = 2.0f;

        if (value != 0)
        {
            float step = value / (t / 0.06f);
            float pre = preValue;

            value = Mathf.Abs(value);

            while (value > 0)
            {
                value -= Mathf.Abs(step);
                pre += (step);
                if ((increase && pre > nextValue) || (!increase && pre < nextValue))
                {
                    pre = nextValue;
                }

                text.text = (int) pre + "";
                yield return new WaitForSecondsRealtime(0.02f);
            }

            text.text = nextValue + "";
        }
        else
        {
            text.text = nextValue + "";
        }

    }
}
