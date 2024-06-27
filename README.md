# KAR Specs
The [Warp Star Eco-System]() introduces various custom file formats for standarization and ease of use in the Kirby Air Ride community.

Any tools developed for handling theses formats must meet theses spects to integrate with the rest of the Eco-System.

## Kirby Air Ride Workshop Quick Install (KWQI) 0.1.0
KWQI is a JSON based text/binary format used for downloading and managing content. From custom ROMs to audio files and tools.

The current spec defines the `SoftwareStage`, `ContentType`, `displayName`, long and short variants of the `description` and if the content is useable for specific OSes, providing a URL for downloading on that OS. And how to manage it's Package.