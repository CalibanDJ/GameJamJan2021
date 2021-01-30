using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace ItemGen
{

public class GrowingRNG
{
    private int _poolSize;
    private int _currentPoolSize;

    private int _growthStep;
    private int _growthThreshold;
    private int _nbGenerationToGrow;
    private bool _isLocked;

    public GrowingRNG(int poolSize, int startPoolSize, int growthStep) {
        Assert.IsTrue(poolSize >= 0, "pool size is always positive, not like you :c \n");
        Assert.IsTrue(startPoolSize >= 0 && startPoolSize < poolSize, "start pool size is not extreme like you, should be between 0 and pool Size \n");
        Assert.IsTrue(growthStep >= 0, "growth step can't be negative >< ! \n");

        // Pool size related 
        _poolSize = poolSize;
        _currentPoolSize = startPoolSize;

        // Growth related
        _growthStep = growthStep;
        _growthThreshold = (_poolSize - _currentPoolSize) * 2; // Threshold needed to grow, can be changed according to need. This is a default value for tests.
        _nbGenerationToGrow = _growthThreshold;
        _isLocked = false;
    }

    public int generateNumber() {
        int nb = Random.Range(0, _currentPoolSize);

        if(!getIsLocked()) {
            feed();
        }

        return nb;
    }

    private void feed() {
        _nbGenerationToGrow--;
        if (_nbGenerationToGrow <= 0) {
            forceGrow();
        }
    }

    public void forceGrow() {
        _currentPoolSize = _currentPoolSize + _growthStep > _poolSize ? _poolSize : _currentPoolSize + _growthStep;
        _nbGenerationToGrow = _growthThreshold;
    }

    public bool getIsLocked() {
        return _isLocked;
    }

    public void lockLeveling() {
        _isLocked = true;
    }

    public void unlockLeveling() {
        _isLocked = false;
    }

    public int getCurrentPoolSize() {
        return _currentPoolSize;
    }
}

}