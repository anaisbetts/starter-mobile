# Starter Project for Mobile Applications

This project is a base project for cross-platform mobile applications that
should help you get started quicker without hours of right-clicking.

<img src="http://cl.ly/image/0s1S460g3k1r/content#png" width=335 />

## Are you using Xamarin.Forms?

**Before** you run the "Get Started" code, run the following command which is
a branch configured to use ReactiveUI-XamForms and creates Xamarin Forms-based
views.

```sh
git reset --hard xam-forms
```

## How do I get started?

```sh
git clone https://github.com/paulcbetts/starter-mobile
script/create NameOfMyNewProject
git remote set-url origin https://github.com/MyUserName/NameOfMyNewProject
git push -u origin master
```

NOTE: Don't use a '.' in any of your project names, iOS gets hella upset. Yes,
I think it'd look way better than '-' too.

## How do I build the project

```sh
script/cibuild
```

## What's the project structure here?

1. Starter-Android: The main Android app project.
1. Starter-iOS: The main iOS (both iPhone and iPad) application project.
1. Starter-Core: Code that is shared between the two projects. This project
   also has Xamarin.Forms set up and ready to go.

# Philosophy

## How does this MVVM Thing Work™?

The idea is, that we want to create our models (i.e. stuff we save to disk and
stuff we send over the network) and ViewModels (a class that represents the
*behavior* of an app screen, by describing how properties are related to each
other) in the Starter-Core library. 

Ideally, the vast majority of our app will live in this library, because
everything we don't put in here we have to write twice, **and** is super hard
to test.

In this app, what we mean by "View" is, on iOS is a UIViewController and
friends, and on Android is usually an Activity. Both of these classes are hard
to test, so we want them to be as dumb as possible.

Here are some things that belong in Starter-Core:

1. Anything related to sending stuff to the network
1. Anything related to loading/saving user's data
1. All of our ViewModel classes (i.e. for every screen in the app, we'll have
   a ViewModel class)

So, it's important that Starter-Core not do platform-specific things - if
you're talking about CGRects in a Starter-Core class, you're Doing It Wrong™.

Here are some things that will end up in Starter-{Platform}

1. The app startup code (i.e. AppDelegate)
1. A ViewController or Activity for every screen in the app. 

# "How Do I?"

## How do I add a new screen to the app?

1. Create a new ViewModel class in Starter-Core and derive it from
   ReactiveObject. Put all of the interesting code in here - calling REST
   APIs, validating stuff, you name it!
1. Walk over to the iOS project, create a new "Universal View Controller",
   copy paste some of the stuff from TestViewController (like the base class
   ReactiveViewController, and that boilerplate ViewModel property at the
   bottom)
1. In the Android project, create a new Activity, same deal around the
   boilerplate there too.

## How do I do interesting stuff in the core library when I can't access UI elements or other stuff??

You should define an Interface, then let the platforms implement. Here's how
you could do this with an example UI action (note that this isn't the best way
to do this, it's just an example):

First, define an interface in the Core project:

```cs
public interface IAlertHelper
{
    void ShowAlert(string message);
}
```

Then, in your ViewModel, you should make it a constructor parameter - we write
it this way so that if we want to replace it with a dummy version in a test
runner, it's easy to do:

```cs
public TestViewModel(IAlertHelper alertHelper = null)
{
    alertHelper = alertHelper ?? Locator.Current.GetService<IAlertHelper>();
    alertHelper.ShowAlert("Wat it do.");
}
```

Now, in the iOS and Android projects, you can register an implementation of this interface:

```cs
public class AppleAlertHelper : IAlertHelper
{
    public void ShowAlert(string message)
    {
        /* ... */
    }
}
```

and in your AppDelegate, you can register it:

```cs
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    Locator.CurrentMutable.RegisterConstant(new AppleAlertHelper(), typeof(IAlertHelper));
}
```
