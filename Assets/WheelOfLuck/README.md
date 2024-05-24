# WheelOfLuck
Provides an opportunity to conveniently add a wheel of fortune to the game.
+ The number of roulette positions is adjusted
+ It is possible to make paid and free scrolls
+ It is possible to set up first n pseudo-random scrolls
+ You can make self realization of specific for project Bonuses.

## How to integrate:
+ Install package from url "https://github.com/RusyaZamorin/WheelOfLuck.git?path=Assets/WheelOfLuck"
  ![Install](https://github.com/RusyaZamorin/WheelOfLuck/assets/59511793/137eab02-b262-40d4-81d3-0c9cc2616d41)

+ look at the samples

+ Implement **IWheelMoneyService** interface.
  Here you need to implement one method: BuyScroll().
  In it, you need to create in-game purchasing of wheel scroll (by usual or special money).

  ![Screenshot_1](https://github.com/RusyaZamorin/WheelOfLuck/assets/59511793/b6a09ab9-a120-4289-93a2-4c91d2ebcdbb)

+ Implement **IWheelPresenter** interface.
  This object is used to visualization the wheel.
  You can use prefab in samples, or create your wheel.

![Screenshot_2](https://github.com/RusyaZamorin/WheelOfLuck/assets/59511793/c3412fb7-1e0c-48b5-bf5d-6df3c25e97de)

+ Realize custom bonuses for your project.
  For this you need to implement **IBonus** interface.

![Screenshot_3](https://github.com/RusyaZamorin/WheelOfLuck/assets/59511793/8cece1a9-dab4-4975-b57c-96d871609cc5)
+ Name - bonus name.
+ Description - short description of bonus. May be used for text visualization of bonus.
+ Type - Consumable or NonConsumable type of bonus
+ Weight - weight for randomizing.
+ Activate() - is called after the bonus has been received.

Example:

![Screenshot_4](https://github.com/RusyaZamorin/WheelOfLuck/assets/59511793/eb4a44a4-b155-4c6c-bc6a-f5188739d1f0)

## How to use:
+ Create **WheelSettings**
+ Create **LuckWheel**:

![Screenshot_5](https://github.com/RusyaZamorin/WheelOfLuck/assets/59511793/26a7c0b9-f3ed-4b8b-aaa3-becafe0c1f58)

+ Call **LuckWheel.Generate()** to setaping wheel content
+ Call **LuckWheel.FreeScroll()** to scroll without paid
+ Call **LuckWheel.PaidScroll(MoneyType)** to try paid and scroll
