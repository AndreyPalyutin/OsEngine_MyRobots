﻿using OsEngine.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace OsEngine.Indicators
{
    [Indicator("AC")]
    internal class AC : Aindicator
    {
        private IndicatorParameterInt _lenghtFastLine;
        private IndicatorParameterInt _lenghtSlowLine;
        public IndicatorParameterString _candlePoint;

        private IndicatorDataSeries _series;

        private Aindicator _ao;

        public override void OnStateChange(IndicatorState state)
        {
            _lenghtFastLine = CreateParameterInt("Fast line length", 5);
            _lenghtSlowLine = CreateParameterInt("Slow line length", 34);
            _candlePoint = CreateParameterStringCollection("Candle point", "Typical", Entity.CandlePointsArray);

            _series = CreateSeries("AC2", Color.DarkGreen, IndicatorChartPaintType.Column, true);

            _ao = IndicatorsFactory.CreateIndicatorByName("AO", Name + "AO", false);
            ((IndicatorParameterInt)_ao.Parameters[0]).Bind(_lenghtFastLine);
            ((IndicatorParameterInt)_ao.Parameters[1]).Bind(_lenghtSlowLine);
            ((IndicatorParameterString)_ao.Parameters[2]).Bind(_candlePoint);
            ProcessIndicator("AO", _ao);
        }

        public override void OnProcess(List<Candle> candles, int index)
        {
            if (index - _lenghtFastLine.ValueInt >= 0 && index - _lenghtSlowLine.ValueInt >= 0)
            {
                _series.Values[index] = GetValue(candles, index);
            }
        }

        private decimal GetValue(List<Candle> candles, int index)
        {
            var smaValue = Summ(_ao.DataSeries[0].Values, index - _lenghtFastLine.ValueInt, index);
            return Math.Round(_ao.DataSeries[0].Values[index] - smaValue / _lenghtFastLine.ValueInt, 6);
        }

        private decimal Summ(List<decimal> values, int startIndex, int endIndex)
        {
            decimal result = 0;

            if (endIndex < startIndex)
            {
                int i = endIndex;
                endIndex = startIndex;
                startIndex = i;
            }

            if (startIndex < 0)
            {
                startIndex = 0;
            }

            if (endIndex >= values.Count)
            {
                endIndex = values.Count - 1;
            }

            for (int i = startIndex + 1; i < endIndex + 1; i++)
            {
                result += values[i];
            }

            return result;
        }
    }
}

