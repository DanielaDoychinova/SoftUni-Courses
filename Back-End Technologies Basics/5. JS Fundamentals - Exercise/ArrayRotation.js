function arrayRotation(arr, n) {
    let effectiveRotations = n % arr.length;

    let rotatedPart = arr.slice(effectiveRotations);
    let rotatedTail = arr.slice(0, effectiveRotations);

    return rotatedPart.concat(rotatedTail).join(' ')
    
}