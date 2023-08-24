ВАЖНО! Чтобы персонаж перемещался анимацией! Не забыть поставить настройки в аниматоре:
Apply Root Motion и Culling Mode - Cull Update Transforms

Заполнение полей
CameraHodler:
			Privot - Pivot (GameObject)
			Character - Перетаскиваем персонажа
			Character Status - scriptable object character status
			Camera Config - scriptable object camera config
			M Transform - Camera Holder (GameObject)
			Cam Trans - Main Camera

CharacterMovement:
			Character Status - scriptable object character status
			Camera Transform - Camera Holder (Game Object)

CameraHodler - верхний пустой GameObject к ней прикрепляется скрипт CameraHandler
CameraHodler расположен в нулевых координатах
	Pivot находится внутри CameraHodker, расположен на правом плече персонажа
		Main Camera перетаскиваем внутрь Pivot, и отводим назад, чтобы персонаж был виден спереди
			TargetLook перетаскиваем внутрь Main Camera и ставим впереди персонажа (это точка куда будет смотреть персонаж)

Также есть настройки камеры и статусы персонажа.
