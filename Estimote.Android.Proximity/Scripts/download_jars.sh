#!/bin/sh

cd "$(dirname ${BASH_SOURCE[0]})"
rm -rf tmp

# step 1: download all the dependencies of the Proximity SDK

./gradlew downloadDependencies
cd tmp

# step 2: extract classes.jar from AARs
#
# Xamarin bindings can only handle one AAR at a time,
# so we leave that spot for the proximity-sdk.aar

for i in `ls *.aar`; do
    unzip -o "$i" -d "$i.unpacked"
    mv "$i.unpacked/classes.jar" "$i.classes.jar"
done

# step 3: prepare a "light" version of kotlin-stdlib
#
# we need to bind some Kotlin APIs for the Proximity APIs
# to be bound properly ... but the Xamarin bindings generator
# can't handle the entire kotlin-stdlib, and silently ignores
# anything beyond just two or three packages
#
# so we prep a "light" version with just the things we need
# to be bound, and feed that to the bindings generator
#
# this "light" version is not included in the final DLL though,
# we include the "full" kotlin-stdlib instead, so that everything
# continues to work just fine at runtime

kotlin_stdlib=`ls kotlin-stdlib-1*`
unzip -o $kotlin_stdlib -d "$kotlin_stdlib.unpacked"
find "$kotlin_stdlib.unpacked/kotlin" -type f -not \( -name 'Unit.class' -or \
	-name 'Function.class' -or -name 'Function0.class' -or -name 'Function1.class' \) -exec rm {} \;
cd "$kotlin_stdlib.unpacked"
zip -r "../$kotlin_stdlib-lite.jar" META-INF kotlin
cd ..

# step 4: finally, we have everything, and can move it to Jars
#
# for now, let's just move all JARs and AARs, even though some
# are not used in the project, because filtering only the things
# we need is too much work :P
#
# and only the things we need are defined in the .csproj, so the
# things we don't need won't show up in Visual Studio anyway

rm proximity-sdk-*.jar
mv *.jar ../../Jars
mv proximity-sdk-*.aar ../../Jars
