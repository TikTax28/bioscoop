﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static System.Console;

//This class is not static so later on we can use inheritance and interfaces
class AccountsLogic
{
    public List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }


    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AccountsAccess.WriteAll(_accounts);

    }

    public AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    public List<AccountModel> GetAllAccounts()
    {
        return _accounts;
    }

    public AccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount;
    }

    public void AddAccount(string emailAddress, string password, string fullName, bool isadmin)
    {
        if (_accounts.Any(f => f.EmailAddress == emailAddress))
        {
            WriteLine("Account with the same details already exists.");
            return;
        }
        AccountModel newAccount = new AccountModel(emailAddress, password, fullName, isadmin);
        _accounts.Add(newAccount);
        AccountsAccess.WriteAll(_accounts);
    }
}