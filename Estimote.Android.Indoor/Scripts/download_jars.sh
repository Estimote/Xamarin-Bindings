#!/bin/sh

# see Estimote.Android.Proximity/Scripts/download_jars.sh for some comments on what's going on here

cd "$(dirname ${BASH_SOURCE[0]})"
rm -rf tmp


./gradlew downloadDependencies
cd tmp


for i in `ls *.aar`; do
    unzip -o "$i" -d "$i.unpacked"
    mv "$i.unpacked/classes.jar" "$i.classes.jar"
done


kotlin_stdlib=`ls kotlin-stdlib-1*`
unzip -o $kotlin_stdlib -d "$kotlin_stdlib.unpacked"
find "$kotlin_stdlib.unpacked/kotlin" -type f -not \( -name 'Unit.class' -or \
	-name 'Function.class' -or -name 'Function0.class' -or -name 'Function1.class' \) -exec rm {} \;
cd "$kotlin_stdlib.unpacked"
zip -r "../$kotlin_stdlib-lite.jar" META-INF kotlin
cd ..


rm indoorsdk-*.jar
mv *.jar ../../Jars
mv indoorsdk-*.aar ../../Jars
