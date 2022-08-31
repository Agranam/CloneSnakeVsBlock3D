using SaveLoadSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class SkinsShop : MonoBehaviour
    {
        [SerializeField] private SkinsSaveData _skinsSaveData;
        [SerializeField] private TailManagment _tailManagment;
        [SerializeField] private GameObject _notEnoughSnakeCells;
        [SerializeField] private TextMeshProUGUI _numberOfCells;
        [SerializeField] private TextMeshProUGUI[] _priceSkinsTextMP;
        [SerializeField] private TextMeshProUGUI[] _pricePlaygroundsTextMP;
        [SerializeField] private int[] _skinsPrice;
        [SerializeField] private int[] _playgroundsPrice;
        [SerializeField] private bool[] _openSkins;
        [SerializeField] private bool[] _openPlaygrounds;

        private void Start()
        {
            if (!_skinsSaveData.IsCreatedSave)
            {
                _skinsSaveData.OpenSkinsWriteData(_openSkins);
                _skinsSaveData.PriceSkinsWriteData(_skinsPrice);
                _skinsSaveData.OpenPlaygroundsWriteData(_openPlaygrounds);
                _skinsSaveData.PricePlaygroundsWriteData(_playgroundsPrice);
                _skinsSaveData.CreatedSave();
            }
            else
            {
                ReadSkinsSaveData();
            }
            SetValue();
        }

        private void ReadSkinsSaveData()
        {
            _skinsSaveData.OpenSkinsReadData(out _openSkins);
            _skinsSaveData.PriceSkinsReadData(out _skinsPrice);
            _skinsSaveData.OpenPlaygroundsReadData(out _openPlaygrounds);
            _skinsSaveData.PricePlaygroundsReadData(out _playgroundsPrice);
            UpdateNumberOfCellsText();
        }

        private void SetValue()
        {
            for (int i = 0; i < _priceSkinsTextMP.Length; i++)
            {
                _priceSkinsTextMP[i].text = _skinsPrice[i].ToString();
                if (_openSkins[i])
                {
                    _priceSkinsTextMP[i].enabled = false;
                    _priceSkinsTextMP[i].GetComponent<Button>().enabled = false;
                }
            }
            for (int i = 0; i < _pricePlaygroundsTextMP.Length; i++)
            {
                _pricePlaygroundsTextMP[i].text = _playgroundsPrice[i].ToString();
                if (_openPlaygrounds[i])
                {
                    _pricePlaygroundsTextMP[i].enabled = false;
                    _pricePlaygroundsTextMP[i].GetComponent<Button>().enabled = false;
                }
            }
        }

        public void SkinsSpendCell(int currentIndex)
        {
            if (_tailManagment.NumberOfCells > 0)
            {
                _tailManagment.FastDeleteCell();
                _skinsPrice[currentIndex]--;
                UpdateNumberOfCellsText();
                _priceSkinsTextMP[currentIndex].text = _skinsPrice[currentIndex].ToString();
                if (_skinsPrice[currentIndex] == 0)
                {
                    _priceSkinsTextMP[currentIndex].enabled = false;
                    _priceSkinsTextMP[currentIndex].GetComponent<Button>().enabled = false;
                    _openSkins[currentIndex] = true;
                    _skinsSaveData.OpenSkinsWriteData(_openSkins);
                }
                _skinsSaveData.PriceSkinsWriteData(_skinsPrice);
            }
            else
            {
                _notEnoughSnakeCells.SetActive(true);
            }
        }
        
        public void PlayGroundsSpendCell(int currentIndex)
        {
            if (_tailManagment.NumberOfCells > 0)
            {
                _tailManagment.FastDeleteCell();
                _playgroundsPrice[currentIndex]--;
                UpdateNumberOfCellsText();
                _pricePlaygroundsTextMP[currentIndex].text = _playgroundsPrice[currentIndex].ToString();
                if (_playgroundsPrice[currentIndex] == 0)
                {
                    _pricePlaygroundsTextMP[currentIndex].enabled = false;
                    _pricePlaygroundsTextMP[currentIndex].GetComponent<Button>().enabled = false;
                    _openPlaygrounds[currentIndex] = true;
                    _skinsSaveData.OpenPlaygroundsWriteData(_openPlaygrounds);
                }

                _skinsSaveData.PricePlaygroundsWriteData(_playgroundsPrice);
            }
            else
            {
                _notEnoughSnakeCells.SetActive(true);
            }
        }

        public void UpdateNumberOfCellsText()
        {
            _numberOfCells.text = _tailManagment.NumberOfCells.ToString();
        }
    }
}
