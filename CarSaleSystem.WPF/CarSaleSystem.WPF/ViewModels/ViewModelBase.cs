﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace CarSaleSystem.WPF.ViewModels;

/// <summary>
/// Base ViewModel for UI components like Window or UserControl with UI.
/// </summary>
public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty] private string _title = "Untitled";

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    public bool IsNotBusy => !IsBusy;
}