# UIElements-Pug
Create Unity UIElements UXML files using [Pug](https://github.com/pugjs/pug). Works great alongside [UIElements-Sass](https://github.com/eidetic-av/UIElements-Sass).

# Why
XML is a pain to write and looks messy. Pug removes the bloat and adds features like variables, functions, loops. 

It is based on [UIElements-Slim](https://github.com/eidetic-av/UIElements-Slim). I couldn't get Slim to work properly on my machine so I decided to try Pug instead. Turns out I also prefer the Pug syntax 😄.

Here is iteration over an array of strings:
```pug
UXML
    Foldout.column(value="true")
        each animal in ['elephant', 'zebra', 'lion', 'giraffe']
            - var elementName = 'row-' + animal
            Box(name=elementName)
                - var imagePath = 'Resources.Load(' + animal + 'Image)'
                Image(class='animal-image' image=imagePath)
                Label(class='animal-label' text=animal)
                - var buttonText = 'Choose the ' + animal
                Button(text=buttonText)
```
It compiles (transpiles?) to the following UXML:
```xml
<?xml version="1.0" encoding="utf-8"?>
<UXML xmlns:xsi="http:/www.w3.org/2001/XMLSchema-instance" xmlns="UnityEngine.UIElements" xsi:noNamespaceSchemaLocation="../UIElementsSchema/UIElements.xsd" xsi:schemaLocation="UnityEngine.UIElements ../UIElementsSchema/UnityEngine.UIElements.xsd">
  <Foldout class="column" value="true">
    <Box name="row-elephant">
      <Image class="animal-image" image="Resources.Load(elephantImage)"></Image>
      <Label class="animal-label" text="elephant"></Label>
      <Button text="Choose the elephant"></Button>
    </Box>
    <Box name="row-zebra">
      <Image class="animal-image" image="Resources.Load(zebraImage)"></Image>
      <Label class="animal-label" text="zebra"></Label>
      <Button text="Choose the zebra"></Button>
    </Box>
    <Box name="row-lion">
      <Image class="animal-image" image="Resources.Load(lionImage)"></Image>
      <Label class="animal-label" text="lion"></Label>
      <Button text="Choose the lion"></Button>
    </Box>
    <Box name="row-giraffe">
      <Image class="animal-image" image="Resources.Load(giraffeImage)"></Image>
      <Label class="animal-label" text="giraffe"></Label>
      <Button text="Choose the giraffe"></Button>
    </Box>
  </Foldout>
</UXML>
```

# Usage
This is just a proof-of-concept so your mileage may vary, but I've tested it on macOS. Currently UIElements-Pug assumes you have a working node installation with Pug installed in `/usr/local/bin`. For Windows, you probably have to modify PugProcessor a bit.

I included [ProcessAsyncHelper](https://gist.github.com/georg-jung/3a8703946075d56423e418ea76212745) for async Process. I modified ProcessAsyncHelper to add support for setting environment variables.

To use it, just copy ProcessAsyncHelper and PugProcessor into your project.

Every time a '.pug' file is added or updated, the script will run the pug compiler and output a '.uxml' file in the same directory.

Included is the above example and a small EditorWindow script that uses said example.

# To-do
* Ideally it wouldn't depend on a separate process in the command line
* Also it would be easier to distribute if Pug was included in some way, instead of it now pointing to the installation on my machine. Good chance it's not going to work out of the box on a different machine.
* I'm not really sure how node/npm works on Windows, but I assume that it doesn't use the UNIXy /usr/local/bin thing. With some preproccesor directives this could be solved, maybe.
