TIMESTAMP?=$(shell date +'%Y%m%d%H%M%S')
DOCKER_TAG?=jaytwo_minirouter

default: clean build

deps:
	dotnet tool install -g dotnet-reportgenerator-globaltool

clean: 
	find . -name bin | xargs --no-run-if-empty rm -vrf
	find . -name obj | xargs --no-run-if-empty rm -vrf
	rm -rf out

restore:
	dotnet restore . --verbosity minimal

build: restore
	dotnet build ./jaytwo.MiniRouter.sln

test: unit-test

unit-test:
	rm -rf out/testResults
	rm -rf out/coverage
	cd ./test/jaytwo.MiniRouter.Tests; \
		dotnet test \
		--results-directory ../../out/testResults \
		--logger "trx;LogFileName=jaytwo.MiniRouter.Tests.trx"
#	TODO: figure out how to get .NET Core 1.1 in the dockerfile
#	cd ./test/jaytwo.MiniRouter.example.AspNetCore1_1.IngegrationTests; \
#		dotnet test \
#		--results-directory ../../out/testResults \
#		--logger "trx;LogFileName=jaytwo.MiniRouter.example.AspNetCore1_1.IngegrationTests.trx";
	cd ./test/jaytwo.MiniRouter.example.AspNetCore2_1.IngegrationTests; \
		dotnet test \
		--results-directory ../../out/testResults \
		--logger "trx;LogFileName=jaytwo.MiniRouter.example.AspNetCore2_1.IngegrationTests.trx";
	cd ./test/jaytwo.MiniRouter.example.AspNetCore3_0.IngegrationTests; \
		dotnet test \
		--results-directory ../../out/testResults \
		--logger "trx;LogFileName=jaytwo.MiniRouter.example.AspNetCore3_0.IngegrationTests.trx";
	reportgenerator \
		-reports:./out/coverage/**/coverage.cobertura.xml \
		-targetdir:./out/coverage/ \
		-reportTypes:Cobertura
	reportgenerator \
		-reports:./out/coverage/**/coverage.cobertura.xml \
		-targetdir:./out/coverage/html \
		-reportTypes:Html
    
pack:
	rm -rf out/packed
	cd ./src/jaytwo.MiniRouter; \
		dotnet pack -o ../../out/packed ${PACK_ARG}

pack-beta: PACK_ARG=--version-suffix beta-${TIMESTAMP}
pack-beta: pack

DOCKER_BUILDER_TAG?=${DOCKER_TAG}__builder
DOCKER_BUILDER_CONTAINER?=${DOCKER_BUILDER_TAG}
docker-build:
	docker build -t ${DOCKER_BUILDER_TAG} . --target builder --pull

DOCKER_RUN_MAKE_TARGETS?=run
docker-run:
	docker run --name ${DOCKER_BUILDER_CONTAINER} ${DOCKER_BUILDER_TAG} make ${DOCKER_RUN_MAKE_TARGETS} || EXIT_CODE=$$? ; \
	docker cp ${DOCKER_BUILDER_CONTAINER}:build/out ./ || echo "Container not found: ${DOCKER_BUILDER_CONTAINER}"; \
	docker rm ${DOCKER_BUILDER_CONTAINER} || echo "Container not found: ${DOCKER_BUILDER_CONTAINER}"}; \
	exit $$EXIT_CODE

docker-unit-test-only: DOCKER_RUN_MAKE_TARGETS=unit-test
docker-unit-test-only: docker-run

docker-unit-test: docker-build docker-unit-test-only

docker-pack-only: DOCKER_RUN_MAKE_TARGETS=pack
docker-pack-only: docker-run

docker-pack: docker-build docker-pack-only

docker-pack-beta-only: DOCKER_RUN_MAKE_TARGETS=pack-beta
docker-pack-beta-only: docker-run

docker-pack-beta: docker-build docker-pack-beta-only

docker-clean:
	docker rm ${DOCKER_BUILDER_CONTAINER} || echo "Container not found: ${DOCKER_BUILDER_CONTAINER}"
	docker rmi ${DOCKER_BUILDER_TAG} || echo "Image not found: ${DOCKER_BUILDER_TAG}"
