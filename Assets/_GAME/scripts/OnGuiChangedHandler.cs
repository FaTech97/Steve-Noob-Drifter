using System;
using System.Collections;
using System.Collections.Generic;
using _GAME.scripts;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.StaticData;
using Shop;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class OnGuiChangedHandler : MonoBehaviour
{
	[SerializeField] private GameObject whileEffectPrefab;
	[SerializeField] private Transform GUI;
	private IPersistanseDataService _persistanseDataService;
	private ItemId _currentId;
	private StaticDataService _staticDataService;
	private SizeAligner _sizeAligner;
	private DriftCarMove _driftCarScript;

	[Inject]
	private void Construct(IPersistanseDataService persistanseDataService, StaticDataService staticDataService)
	{
		_persistanseDataService = persistanseDataService;
		_staticDataService = staticDataService;
	}

	private void Awake()
	{
		_sizeAligner = GetComponent<SizeAligner>();
		_driftCarScript = GetComponent<DriftCarMove>();
	}

	private void Start()
	{
		SpawnCar(_persistanseDataService.Data.Player.CurrectItemId);
		_persistanseDataService.OnDataChanged += CheckNewValue;
	}

	private void OnDestroy()
	{
		_persistanseDataService.OnDataChanged -= CheckNewValue;
	}

	private void CheckNewValue()
	{
		if (_currentId != _persistanseDataService.Data.Player.CurrectItemId)
		{
			SpawnCar(_persistanseDataService.Data.Player.CurrectItemId);
		}
	}

	private void SpawnCar(ItemId playerCurrectItemId)
	{
		if (GUI.childCount != 0)
		{
			Destroy(GUI.GetChild(0).gameObject);
		}

		_currentId = playerCurrectItemId;
		ShopItemConfig newItem = _staticDataService.Items.Get(playerCurrectItemId);
		_driftCarScript.SetNewConfig(newItem);
		GameObject gameObject = Instantiate(newItem.model, GUI);
		_sizeAligner.AlignColliderSizeToObjectSize(gameObject);
		SpawnWheelsShadow();
	}

	private void SpawnWheelsShadow()
	{
		var _collider = GetComponent<BoxCollider>();
		// FOR FRONT WHEELS EFFECT
		// SpawnOneShadow(leftSpawn, "left");
		// SpawnOneShadow(rightSpawn, "right");
		// var leftSpawn = new Vector3(_collider.center.x - _collider.size.x / 2, _collider.center.y - _collider.size.y / 2,
		//     _collider.center.z + _collider.size.z / 2);
		// var rightSpawn = new Vector3(_collider.center.x + _collider.size.x / 2, _collider.center.y - _collider.size.y / 2,
		//     _collider.center.z + _collider.size.z / 2);
		var leftBackSpawn = new Vector3(_collider.center.x - _collider.size.x / 2 + 0.5f, _collider.center.y - _collider.size.y / 2,
			_collider.center.z - _collider.size.z / 2 + 1f);
		var rightBackSpawn = new Vector3(_collider.center.x + _collider.size.x / 2 - 0.5f, _collider.center.y - _collider.size.y / 2,
			_collider.center.z - _collider.size.z / 2 + 1f);
		SpawnOneShadow(leftBackSpawn, "leftBack");
		SpawnOneShadow(rightBackSpawn, "rightBack");
	}

	private void SpawnOneShadow(Vector3 spawnPosition, string name)
	{
		GameObject newObject =
			Instantiate(whileEffectPrefab, Vector3.zero, Quaternion.identity);
		newObject.name = name;
		newObject.transform.parent = transform;
		newObject.transform.localPosition = spawnPosition;
		newObject.transform.parent = transform.GetChild(0);
	}
}
