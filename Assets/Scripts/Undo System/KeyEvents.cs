using UnityEngine;
using System.Collections.Generic;

namespace Zong
{
    public class KeyEvent :ICommand
    {
        FirstPersonController _player;
        GameObject _mainUI;
        Stack<Vector3> _positionStack=new Stack<Vector3>();
        List<ParticleSystem> _particleSystems=new List<ParticleSystem>();
        Stack<Item> _itemStack=new Stack<Item>();
        Inventory _inventory;
        UI_Inventory _UI_Inventory;

        /// <summary>
        /// Set the KeyEvent
        /// </summary>
        /// <param name="player"></param>
        /// <param name="checkPointList"></param>
        /// <param name="particleSystems"></param>
        /// <param name="inventory"></param>
        /// <param name="item"></param>
        /// <param name="uI_Inventory"></param>
        public KeyEvent(FirstPersonController player, List<GameObject> checkPointList,List<ParticleSystem> particleSystems,Inventory inventory,Item item,UI_Inventory uI_Inventory)
        {
            _player = player;
            foreach (var checkPoint in checkPointList)
            {
                _positionStack.Push(checkPoint.transform.position);
            }
            _particleSystems = particleSystems;
            _inventory = inventory;
            _itemStack.Push(item);
            _UI_Inventory = uI_Inventory;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        public void Execute()
        {
            _UI_Inventory.AddItemAndSetUI(_itemStack.Peek());
        }

        /// <summary>
        /// Undo's commands in the stack
        /// </summary>
        public void Undo()
        {
            Vector3 resetedPosition = _positionStack.Pop();
            _player.MoveToSpecificCheckPoint(resetedPosition);
            _UI_Inventory.AddItemAndSetUI(_itemStack.Pop());
            foreach(var item in _particleSystems)
            {
                item.Stop();
            }
        }

    }
}


