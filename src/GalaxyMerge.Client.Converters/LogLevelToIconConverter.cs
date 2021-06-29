﻿using System;
using System.Globalization;
using System.Windows.Controls;
using GalaxyMerge.Client.Core.Converters;
using GalaxyMerge.Client.Core.Utilities;
using NLog;

namespace GalaxyMerge.Client.Converters
{
    public class LogLevelToIconConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not LogLevel logLevel)
                throw new InvalidOperationException("Value must be of type LogLevel");

            if (logLevel == LogLevel.Info)
                return ResourceFinder.Find<ControlTemplate>("Icon.Info");
            if (logLevel == LogLevel.Warn)
                return ResourceFinder.Find<ControlTemplate>("Icon.Warning");
            if (logLevel == LogLevel.Error)
                return ResourceFinder.Find<ControlTemplate>("Icon.Error");

            return null;
        }
    }
}